using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Text;
using System.Net.Http;

namespace orchestrator.Controllers
{
    [Route("api/send")]
    public class SendController : Controller
    {
        private readonly IHttpClientFactory clientFactory;

        public SendController(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }

        [HttpGet]
        public async Task<string> Get()
        {
            return await SendHttp();
        }

        private async Task<string> SendHttp()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "http://gateway:80/card/unload");
            var client = clientFactory.CreateClient();

            var response = await client.SendAsync(request);
            var contents = await response.Content.ReadAsStringAsync();

            Console.WriteLine(response.StatusCode);
            Console.WriteLine(contents);

            if (response.IsSuccessStatusCode)
            {
                return contents;
            }
            else
            {
                return "Failed";
            }
        }

        private void SendMessage()
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
