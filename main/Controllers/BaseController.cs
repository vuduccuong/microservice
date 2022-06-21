using Microsoft.AspNetCore.Mvc;
using main.Helpers;

namespace main.Controllers;

[ApiController]
public class BaseController : ControllerBase
{
    protected ActionResult HandlerResult<T>(ResultBase<T> result)
    {
        if (result == null)
            return NotFound();

        if (!result.IsSuccess)
            return BadRequest();

        if (result.Response == null)
            return NotFound();

        return Ok(result.Response);
    }
}