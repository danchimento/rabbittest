using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Text;

namespace card.Controllers
{
    [Route("api/card")]
    public class CardController : Controller
    {
        [HttpGet]
        [Route("unload")]
        public async Task<string> Unload()
        {
            Console.WriteLine("Beginning card unload...");
            return "Card unloaded successfully";
        }
    }
}
