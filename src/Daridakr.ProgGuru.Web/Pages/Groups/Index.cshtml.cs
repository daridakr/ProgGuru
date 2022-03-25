using Daridakr.ProgGuru.Groups;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Daridakr.ProgGuru.Web.Pages.Groups
{
    public class IndexModel : ProgGuruPageModel
    {
        public IReadOnlyList<GroupDto> Groups { get; set; }

        private readonly IGroupAppService _groupAppService;

        public IndexModel(IGroupAppService groupAppService)
        {
            _groupAppService = groupAppService;
        }

        public async Task OnGetAsync()
        {
            var result = await _groupAppService.GetListAsync(new GetGroupListDto { Filter = null });
            Groups = result.Items;
        }
    }
}
