using BallastLane.ApplicationService;
using BallastLane.ApplicationService.Interface;
using BallastLane.ApplicationService.Mapping;
using BallastLane.Domain.Settings;
using BallastLane.Infrastructure.Data.Repository;
using BallastLane.Infrastructure.Data.Repository.Interface;
using BallastLane.ApplicationService.Interface;
using Domain.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace BallastLane.Infrastructure.IoC
{
    public static class ServiceDependency
    {
        public static IProjectSettings AddSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = new ProjectSettings
            {
                DatabaseConnectionString = configuration["Database:ConnectionString"],
                DatabaseName = configuration["Database:Name"],
            };
            services.AddSingleton<IProjectSettings>(settings);
            return settings;
        }

        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddScoped<IApplicationAppService, ApplicationAppService>();
            services.AddScoped<IUserAppService, UserAppService>();
            services.AddAutoMapper(typeof(AutoMapperProfile));
            return services;
        }

        public static IServiceCollection AddInfrastructureData(this IServiceCollection services, IProjectSettings projectSettings)
        {
            services.AddScoped<IApplicationRepository, ApplicationRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }

        public static IMongoClient AddMongoClient(this IServiceCollection services, IProjectSettings projectSettings)
        {
            var mongoClient = new MongoClient(projectSettings.DatabaseConnectionString);
            services.AddSingleton<IMongoClient>(mongoClient);
            return mongoClient;
        }

        public static IServiceCollection AddMongoDatabase(this IServiceCollection services, IMongoClient mongoClient, IProjectSettings projectSettings)
        {
            var mongoDatabase = mongoClient.GetDatabase(projectSettings.DatabaseName);
            services.AddScoped(provider => mongoDatabase);
            return services;
        }
    }
}
