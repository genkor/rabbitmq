using System;
using System.Text;
using RabbitMQ.Client;

var factory = new ConnectionFactory { HostName = "192.168.30.171", UserName = "producer", Password = "123456" };

using var connection = factory.CreateConnection();

using var channel = connection.CreateModel();

var exchangeName = "letter-exchange";

channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Fanout);

var message = $"Brodcastings a Message at {DateTime.Now.ToLocalTime()}.";
var encodedMessage = Encoding.UTF8.GetBytes(message);

channel.BasicPublish(exchange: exchangeName, "", null, encodedMessage);

Console.WriteLine($"Published message: '{message}' on exchange '{exchangeName}'");

Console.WriteLine(" Press [enter] to exit.");
Console.ReadKey();
