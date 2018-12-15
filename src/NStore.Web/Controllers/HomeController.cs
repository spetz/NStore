using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NStore.Web.Framework;

namespace NStore.Web.Controllers
{
    [Route("api")]
    public class HomeController : ControllerBase
    {
        private readonly IOptions<AppOptions> _appOptions;

        public HomeController(IOptions<AppOptions> appOptions)
        {
            _appOptions = appOptions;
        }

        [HttpGet]
        public ActionResult<string> Get()
            => $"Welcome to {_appOptions.Value.Name} API";
    }
}