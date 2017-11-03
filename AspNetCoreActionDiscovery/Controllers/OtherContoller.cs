using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreActionDiscovery.Controllers
{
    [Route("api/other")]
    [Authorize]
    public class OtherContoller : ControllerBase
    {
        [Route("test/{id:int}")]
        [AllowAnonymous]
        public IActionResult Test(int id) => Ok("Test");
    }
}
