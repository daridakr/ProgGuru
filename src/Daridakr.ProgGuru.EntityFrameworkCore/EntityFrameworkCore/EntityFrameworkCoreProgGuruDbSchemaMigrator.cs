using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Daridakr.ProgGuru.Data;
using Volo.Abp.DependencyInjection;

namespace Daridakr.ProgGuru.EntityFrameworkCore;

public class EntityFrameworkCoreProgGuruDbSchemaMigrator
    : IProgGuruDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreProgGuruDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the ProgGuruDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<ProgGuruDbContext>()
            .Database
            .MigrateAsync();
    }
}
