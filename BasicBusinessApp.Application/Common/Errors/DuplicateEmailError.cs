using System.Net;

namespace BasicBusinessApp.Application.Common.Errors;

public record struct DuplicateEmailError() : IError
{
  public HttpStatusCode StatusCode => HttpStatusCode.Conflict;

  public string ErrorMessage => "err";
}
