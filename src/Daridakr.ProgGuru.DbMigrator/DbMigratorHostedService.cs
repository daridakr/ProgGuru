using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Daridakr.ProgGuru.Data;
using Serilog;
using Volo.Abp;
using System;

namespace Daridakr.ProgGuru.DbMigrator;

public class DbMigratorHostedService : IHostedService
{
    private readonly IHostApplicationLifetime _hostApplicationLifetime;
    private readonly IConfiguration _configuration;

    public DbMigratorHostedService(IHostApplicationLifetime hostApplicationLifetime, IConfiguration configuration)
    {
        _hostApplicationLifetime = hostApplicationLifetime;
        _configuration = configuration;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using (var application = await AbpApplicationFactory.CreateAsync<ProgGuruDbMigratorModule>(options =>
        {
            options.UseAutofac();
            options.Services.AddLogging(c => c.AddSerilog());

            //options.Services.ReplaceConfiguration(_configuration);
            options.Services.ReplaceConfiguration(BuildConfiguration());
        }))
        {
            await application.InitializeAsync();

            await application
                .ServiceProvider
                .GetRequiredService<ProgGuruDbMigrationService>()
                .MigrateAsync();

            await application.ShutdownAsync();

            _hostApplicationLifetime.StopApplication();
        }
    }

    private static IConfiguration BuildConfiguration()
    {
        var configurationBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json");

        // Extra code block to make it possible to read from appsettings.Staging.json
        //var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        //if (environmentName == "Staging")
        //{
        //    configurationBuilder.AddJsonFile($"appsettings.{environmentName}.json", true);
        //}

        configurationBuilder.AddJsonFile($"appsettings.Staging.json", true);

        return configurationBuilder
            .AddEnvironmentVariables()
            .Build();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
