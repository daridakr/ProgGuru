using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Daridakr.ProgGuru.Web;

[Dependency(ReplaceServices = true)]
public class ProgGuruBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "ProgGuru";
}
