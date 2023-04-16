using System;
using System.Text;
using RabbitMQ.Client;

var factory = new ConnectionFactory { HostName = "192.168.30.171" };

using var connection = factory.CreateConnection();

using var channel = connection.CreateModel();

var queueName = "letterbox";
channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

var message = "This is my first Message.";
var encodedMessage = Encoding.UTF8.GetBytes(message);

channel.BasicPublish("", queueName, null, encodedMessage);

Console.WriteLine($"Published message: '{message}' on queue '{queueName}'");
