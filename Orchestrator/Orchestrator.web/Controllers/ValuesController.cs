using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Orchestrator.Communication;

namespace Orchestrator.web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ICommunicator communicator;

        public ValuesController(ICommunicator communicator)
        {
            this.communicator = communicator;
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
        //    await communicator.Send("fuckin' nailed it.");

            return new string[] { "value1", "value2" };
        }
    }
}
