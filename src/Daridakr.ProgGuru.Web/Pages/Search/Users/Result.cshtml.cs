using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Identity;

namespace Daridakr.ProgGuru.Web.Pages.Search.Users
{
    public class ResultModel : ProgGuruPageModel
    {
        public IReadOnlyList<IdentityUserDto> Users { get; set; }

        private readonly IIdentityUserAppService _userAppService;

        public ResultModel(IIdentityUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        public async Task<IActionResult> OnGetAsync(string searchString)
        {
            if (searchString != null)
            {
                if (CurrentUser.IsAuthenticated)
                {
                    var result = await _userAppService.GetListAsync(new GetIdentityUsersInput { Filter = searchString });
                    Users = result.Items;
                }
                else return Redirect("/account/login");
            }
            return Page();
        }
    }
}
