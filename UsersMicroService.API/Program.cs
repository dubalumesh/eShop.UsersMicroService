using UsersMicroService.Core;
using UsersMicroService.Infrastructure;
using Swashbuckle.AspNetCore.Swagger;
using UsersMicroService.API.Handler;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCore();
builder.Services.AddInfrastructure();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().AddJsonOptions(option =>
{
    option.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
});
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails(); //




var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Users API V1");
});

app.Run();
