using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using Volo.Abp.Identity;

namespace Daridakr.ProgGuru.Web.Pages
{
    [Authorize]
    public class ChatModel : ProgGuruPageModel
    {
        public void OnGet()
        {

        }
    }
}
