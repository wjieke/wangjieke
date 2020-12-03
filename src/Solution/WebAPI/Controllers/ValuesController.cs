using log4net;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace WebAPI.Controllers
{
    //启动浏览器默认：api/values

    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ILog log;
        private readonly ILogger<ValuesController> _logger;

        public ValuesController(ILogger<ValuesController> logger)
        {
            log = LogManager.GetLogger(Startup.LoggerRepository.Name, typeof(ValuesController));
            _logger = logger;
        }

        // GET api/values
        [HttpGet]
        //[HttpGet, Route("Get")]
        public ActionResult<IEnumerable<string>> Get()
        {
            log.Error("测试日志");
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return id + "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {

            log.Error(value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            log.Error(value);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            log.Error(id);
        }
    }
}