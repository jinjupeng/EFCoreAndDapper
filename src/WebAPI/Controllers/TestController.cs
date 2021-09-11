using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebAPI.BLL;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ITestService _testService;

        public TestController(ITestService testService)
        {
            _testService = testService;
        }

        [HttpGet]
        [Route("insert")]
        public async Task<IActionResult> InsertTest()
        {
            await _testService.InsertAsync();
            return Ok();
        }

        [HttpGet]
        [Route("getuser")]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await Task.FromResult(_testService.GetUsers()));
        }

        [HttpGet]
        [Route("getorg")]
        public async Task<IActionResult> GetOrgss()
        {
            return Ok(await Task.FromResult(_testService.GetOrgs()));
        }
    }
}
