using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public Task<ActionResult> Test1()
        {
            throw new Exception("我就是要报个错");
        }

        [HttpGet]
        public Task Test2()
        {
            Console.WriteLine("我是正常执行");
            return Task.CompletedTask;
        }
    }
}
