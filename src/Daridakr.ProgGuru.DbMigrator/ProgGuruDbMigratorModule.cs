using Daridakr.ProgGuru.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace Daridakr.ProgGuru.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(ProgGuruEntityFrameworkCoreModule),
    typeof(ProgGuruApplicationContractsModule)
    )]
public class ProgGuruDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
    }
}
