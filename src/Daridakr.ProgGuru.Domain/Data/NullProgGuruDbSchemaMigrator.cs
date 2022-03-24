using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Daridakr.ProgGuru.Data;

/* This is used if database provider does't define
 * IProgGuruDbSchemaMigrator implementation.
 */
public class NullProgGuruDbSchemaMigrator : IProgGuruDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
