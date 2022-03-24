using Daridakr.ProgGuru.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Daridakr.ProgGuru.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class ProgGuruController : AbpControllerBase
{
    protected ProgGuruController()
    {
        LocalizationResource = typeof(ProgGuruResource);
    }
}
