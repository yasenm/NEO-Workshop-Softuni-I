using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Neo.Ledger;
using Neo.Network.P2P;
using Neo.Plugins;

namespace LiveNodeTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet("[action]")]
        public ActionResult<IActionResult> GetRemoteNodes()
        {
            var nodes = LocalNode.Singleton.GetRemoteNodes().Select(x => x.Version);
            return Ok(nodes);
        }

        // GET api/values/5
        [HttpGet]
        public ActionResult<string> GetHeight()
        {
            var height = Blockchain.Singleton.Height.ToString();
            return height;
        }
    }
}
