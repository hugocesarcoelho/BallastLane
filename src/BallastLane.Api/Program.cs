using BallastLane.Infrastructure.IoC;

var builder = WebApplication.CreateBuilder(args);

var settings = builder.Services.AddSettings(builder.Configuration);
var mongoClient = builder.Services.AddMongoClient(settings);

builder.Services
    .AddMongoDatabase(mongoClient, settings)
    .AddEndpointsApiExplorer()
    .AddRouting(options => options.LowercaseUrls = true)
    .AddSwaggerGen()
    .AddApplicationService()
    .AddInfrastructureData(settings)
    .AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
