using BasicBusinessApp.Common.Errors;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BasicBusinessApp.Api.Controllers;

public class ErrorsContorollers : ControllerBase
{
  [Route("/error")]
  public IActionResult Error()
  {
    var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
    var (statusCode, message) = exception switch
    {
      DuplicateEmailException => (StatusCodes.Status409Conflict, "Email already exists"),
      _ => (StatusCodes.Status500InternalServerError, "An error occurred while processing your request.")
    };
    return Problem(statusCode: statusCode, title: message);
  }
}
