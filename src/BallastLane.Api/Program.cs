using BallastLane.Api.Extensions;
using BallastLane.Infrastructure.IoC;
using Findox.WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var settings = services.AddSettings(builder.Configuration);
var mongoClient = services.AddMongoClient(settings);

services
    .AddMongoDatabase(mongoClient, settings)
    .AddEndpointsApiExplorer()
    .AddRouting(options => options.LowercaseUrls = true)
    .AddSwaggerGen()
    .AddApplicationService()
    .AddInfrastructureData(settings)
    .AddControllers(options =>
    {
        options.Filters.Add(new HttpResponseExceptionFilter());
    });

services.AddSwagger();
services.AddAuthenticationService();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
