using Volo.Abp.Modularity;

namespace Daridakr.ProgGuru;

[DependsOn(
    typeof(ProgGuruApplicationModule),
    typeof(ProgGuruDomainTestModule)
    )]
public class ProgGuruApplicationTestModule : AbpModule
{

}
