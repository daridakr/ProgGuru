using Daridakr.ProgGuru.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Daridakr.ProgGuru.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class ProgGuruPageModel : AbpPageModel
{
    protected ProgGuruPageModel()
    {
        LocalizationResourceType = typeof(ProgGuruResource);
    }
}
