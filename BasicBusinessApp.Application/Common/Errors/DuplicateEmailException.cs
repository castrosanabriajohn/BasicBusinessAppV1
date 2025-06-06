using System.Net;

namespace BasicBusinessApp.Common.Errors;

public class DuplicateEmailException : Exception, IServiceException
{
  public HttpStatusCode StatusCode => HttpStatusCode.Conflict;

  public string ErrorMessage => "Email already exists";
}
