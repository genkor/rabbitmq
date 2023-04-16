using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var factory = new ConnectionFactory { HostName = "192.168.30.171", UserName = "consumer", Password = "123456" };

using var connection = factory.CreateConnection();

using var channel = connection.CreateModel();

var queueName = "letterbox";
channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"Message Received: '{message}' on queue '{queueName}'");
};

channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

Console.WriteLine(" Press [enter] to exit.");
Console.ReadKey();
