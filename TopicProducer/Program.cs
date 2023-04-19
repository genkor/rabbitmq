using System;
using System.Text;
using RabbitMQ.Client;

var factory = new ConnectionFactory { HostName = "192.168.30.171", UserName = "producer", Password = "123456" };

using var connection = factory.CreateConnection();

using var channel = connection.CreateModel();

var exchangeName = "topic-exchange";
var analyticsKey = "analyticsonly";
var paymentsKey = "paymentssonly";

channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Direct);

var message1 = $"Brodcastings a Message for: [{analyticsKey}]";
var encodedMessage1 = Encoding.UTF8.GetBytes(message1);
channel.BasicPublish(exchange: exchangeName, routingKey: analyticsKey, null, encodedMessage1);
Console.WriteLine($"Published message: '{message1}' on exchange '{exchangeName}' with routing key: '{analyticsKey}'");

var message2 = $"Brodcastings a Message for: [{paymentsKey}]";
var encodedMessage2 = Encoding.UTF8.GetBytes(message1);
channel.BasicPublish(exchange: exchangeName, routingKey: paymentsKey, null, encodedMessage2);
Console.WriteLine($"Published message: '{message2}' on exchange '{exchangeName}' with routing key: '{paymentsKey}'");

Console.WriteLine(" Press [enter] to exit.");
Console.ReadKey();
