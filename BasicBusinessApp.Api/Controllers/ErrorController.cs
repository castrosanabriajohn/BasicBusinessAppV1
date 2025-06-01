using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BasicBusinessApp.Api.Controllers;

public class ErrorsContorollers : ControllerBase
{
  [Route("/error")]
  public IActionResult Error()
  {
    var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
    return Problem();
  }
}
