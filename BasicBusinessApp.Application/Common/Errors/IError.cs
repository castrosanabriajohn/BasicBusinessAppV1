using System.Net;

namespace BasicBusinessApp.Application.Common.Errors;

public interface IError
{
  public HttpStatusCode StatusCode { get; }
  public string ErrorMessage { get; }
}
