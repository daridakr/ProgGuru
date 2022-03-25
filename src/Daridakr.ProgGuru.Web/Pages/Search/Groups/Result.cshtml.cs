using Daridakr.ProgGuru.Groups;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Daridakr.ProgGuru.Web.Pages.Search.Groups
{
    public class ResultModel : ProgGuruPageModel
    {
        public IReadOnlyList<GroupDto> Groups { get; set; }

        private readonly IGroupAppService _groupAppService;

        public ResultModel(IGroupAppService groupAppService)
        {
            _groupAppService = groupAppService;
        }

        public async Task<IActionResult> OnGetAsync(string searchString)
        {
            if (searchString != null)
            {
                if (CurrentUser.IsAuthenticated)
                {
                    var result = await _groupAppService.GetListAsync(new GetGroupListDto { Filter = searchString });
                    Groups = result.Items;
                }
                else return Redirect("/account/login");
            }
            return Page();
        }
    }
}
