using RabbitMQ.Client;

namespace Shared
{
    public class ConnectionHelper
    {
        public static ConnectionFactory Factory = new ConnectionFactory
        {
            HostName = "langbui-rabbitmq.southeastasia.azurecontainer.io",
            UserName = "guest",
            Password = "N%?2O>Cdf,4fy?dUae,S"
        };
    }
}