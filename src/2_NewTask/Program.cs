using System;
using RabbitMQ.Client;
using System.Text;

class Program
{
    public static void Main(string[] args)
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using(var connection = factory.CreateConnection())
        using(var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: "task_queue1", durable: true, exclusive: false, autoDelete: false, arguments: null);
            while (true)
            {
                var message = Console.ReadLine();
                var body = Encoding.UTF8.GetBytes(message);

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                channel.BasicPublish(exchange: "", routingKey: "task_queue1", basicProperties: properties, body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }
        }
    }
}
