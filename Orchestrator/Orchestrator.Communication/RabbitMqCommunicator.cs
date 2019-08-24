using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Orchestrator.Communication
{
    public class RabbitMqCommunicator : ICommunicator
    {
        private readonly ConnectionFactory connectionFactory;

        public RabbitMqCommunicator(ConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public async Task Send<T>(T request)
        {
            using (var connection = connectionFactory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "cardunloads",
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

                string message = "Unload card";
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "hello",
                                     basicProperties: null,
                                     body: body);

                Console.WriteLine(" [x] Sent {0}", message);
            }
        }
    }
}
