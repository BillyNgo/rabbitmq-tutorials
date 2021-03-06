using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Shared;

class Program
{
    public static void Main()
    {
        //var factory = new ConnectionFactory() { HostName = "localhost" };
        var factory = ConnectionHelper.Factory;
        using (var connection = factory.CreateConnection())
        using(var channel = connection.CreateModel())
        {
            channel.ExchangeDeclare(exchange: "fanout_logs", type: ExchangeType.Fanout);

            var queueName = channel.QueueDeclare("logQueue_First", false, false).QueueName;
            channel.QueueBind(queue: queueName, exchange: "fanout_logs", routingKey: "");

            Console.WriteLine(" [*] Waiting for logs.");

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                byte[] body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" [x] {0}", message);
            };
            channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
