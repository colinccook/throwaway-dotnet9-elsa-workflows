using Elsa.Extensions;
using ElsaWeb.Workflows;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddElsa(elsa =>
{
    elsa.AddWorkflow<HttpHelloWorld>();
    elsa.AddWorkflow<ValidateOpeningRead>();
    elsa.UseHttp();

});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseWorkflows();
app.Run();