using BasicBusinessApp.Api.Common.Errors;
using BasicBusinessApp.Application;
using BasicBusinessApp.Infrastructure;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration);
    builder.Services.AddControllers();
    builder.Services.AddSingleton<ProblemDetailsFactory, DetailsFactory>();
}

var app = builder.Build();
{
    app.UseExceptionHandler("/error");
    
    app.UseHttpsRedirection();
    app.MapControllers(); 
    app.Run(); 
}
