// using BasicBusinessApp.Api.Filters;

using BasicBusinessApp.Api.Errors;
using BasicBusinessApp.Application;
using BasicBusinessApp.Infrastructure;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration);
    builder.Services.AddControllers(); // options => options.Filters.Add<ErrorHandlingFilterAttribute>()
    builder.Services.AddSingleton<ProblemDetailsFactory, DetailsFactory>();
}

var app = builder.Build();
{
    // app.UseMiddleware<ErrorHandlingMiddleware>();
    app.UseExceptionHandler("/error");
    app.Map("/error", (HttpContext httpContext) =>
    {
        Exception? exception = httpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
        return Results.Problem();
    });
    app.UseHttpsRedirection();
    app.MapControllers(); 
    app.Run(); 
}
