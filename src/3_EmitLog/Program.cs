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
            channel.ExchangeDeclare(exchange: "fanout_logs", type: ExchangeType.Fanout);
            while (true)
            {
                var message = Console.ReadLine();
                var body = Encoding.UTF8.GetBytes(message);
                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;
                channel.BasicPublish(exchange: "fanout_logs", routingKey: "", basicProperties: properties, body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }
      
        }
    }
}
