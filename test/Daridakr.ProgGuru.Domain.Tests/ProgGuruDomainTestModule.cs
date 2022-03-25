using Daridakr.ProgGuru.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Daridakr.ProgGuru;

[DependsOn(
    typeof(ProgGuruEntityFrameworkCoreTestModule)
    )]
public class ProgGuruDomainTestModule : AbpModule
{

}
