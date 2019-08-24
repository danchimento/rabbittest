using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using aspnetapp.Models;
using RabbitMQ.Client;
using System.Text;

namespace aspnetapp.Controllers
{
    [Route("api/send")]
    public class SendController : Controller
    {
        [HttpGet]
        public async Task<string> Get()
        {
            Send();
            return "Sent";
        }

        private void Send()
        {
            var factory = new ConnectionFactory() { HostName = "rabbit" };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "cardunload",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string message = "Hello World!";
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "cardunload",
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }
        }
    }
}
