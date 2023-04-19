using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var factory = new ConnectionFactory { HostName = "192.168.30.171", UserName = "consumer", Password = "123456" };

using var connection = factory.CreateConnection();

using var channel = connection.CreateModel();

var exchangeName = "letter-exchange";

channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Fanout);

var queueName = channel.QueueDeclare().QueueName;

channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: "");

var consumer = new EventingBasicConsumer(channel);

consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($" [x] First Consumer: Received Message '{message}' on queue '{queueName}'");
};

channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

Console.WriteLine(" Press [enter] to exit.");
Console.ReadKey();
