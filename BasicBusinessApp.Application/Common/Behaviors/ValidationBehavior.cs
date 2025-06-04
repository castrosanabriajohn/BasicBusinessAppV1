using BasicBusinessApp.Application.Authentication.Commands.Register;
using BasicBusinessApp.Application.Authentication.Common;
using ErrorOr;
using FluentValidation;
using MediatR;

namespace BasicBusinessApp.Application.Common.Behaviors;

public class ValidateRegisterCommandBehavior : IPipelineBehavior<RegisterCommand, ErrorOr<AuthenticationResult>>
{
  private readonly IValidator<RegisterCommand> _validator;

  public ValidateRegisterCommandBehavior(IValidator<RegisterCommand> validator) => _validator = validator;

  public async Task<ErrorOr<AuthenticationResult>> Handle(
    RegisterCommand request, RequestHandlerDelegate<ErrorOr<AuthenticationResult>> next,
    CancellationToken cancellationToken)
  {
    var validationResult = await _validator.ValidateAsync(request, cancellationToken);
    if (validationResult.IsValid)
    {
      return await next(); // call handler
    }
    var errors = validationResult.Errors
      .ConvertAll(validationError => Error.Validation(validationError.PropertyName, validationError.ErrorMessage)).ToList();  
    return errors;
  }
}