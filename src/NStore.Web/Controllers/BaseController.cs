using Microsoft.AspNetCore.Mvc;

namespace NStore.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        protected ActionResult<T> Result<T>(T value)
        {
            if (value == null)
            {
                return NotFound();
            }

            return value;
        }
    }
}