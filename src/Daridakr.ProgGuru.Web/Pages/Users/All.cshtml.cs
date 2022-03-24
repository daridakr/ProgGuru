using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Identity;

namespace Daridakr.ProgGuru.Web.Pages.Users
{
    public class AllModel : ProgGuruPageModel
    {
        public IReadOnlyList<IdentityUserDto> Users { get; set; }

        private readonly IIdentityUserAppService _userAppService;

        public AllModel(IIdentityUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        public async Task OnGetAsync()
        {
            var result = await _userAppService.GetListAsync(new GetIdentityUsersInput { Filter = null });
            Users = result.Items;
        }
    }
}
