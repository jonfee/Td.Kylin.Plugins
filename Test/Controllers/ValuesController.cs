using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Td.Kylin.Message.Sender;
using Td.Kylin.SMS.Sender;

namespace Test.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            UserLevelUpMessageSender acSender = new UserLevelUpMessageSender(100);
            //acSender.Send();

            RegistValidateCodeSmsSender rcSender = new RegistValidateCodeSmsSender("15920005942", "1234", 2);
            //var result = rcSender.SendAsync().Result;

            return new string[] { acSender.Template, acSender.Content };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
