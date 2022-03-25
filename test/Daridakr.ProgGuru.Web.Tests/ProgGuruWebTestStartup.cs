using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Volo.Abp;

namespace Daridakr.ProgGuru;

public class ProgGuruWebTestStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApplication<ProgGuruWebTestModule>();
    }

    public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
    {
        app.InitializeApplication();
    }
}
