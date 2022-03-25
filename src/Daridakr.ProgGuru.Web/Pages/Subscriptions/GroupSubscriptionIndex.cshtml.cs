using Daridakr.ProgGuru.Subscriptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Daridakr.ProgGuru.Web.Pages.Subscriptions
{
    public class GroupSubscriptionIndexModel : ProgGuruPageModel
    {
        public IReadOnlyList<AdminGroupSubscriptionDto> GroupSubscriptions { get; set; }

        private readonly IGroupSubscriptionAppService _groupSubscriptionAppService;

        public GroupSubscriptionIndexModel(IGroupSubscriptionAppService groupSubscriptionAppService)
        {
            _groupSubscriptionAppService = groupSubscriptionAppService;
        }

        public async Task OnGetAsync()
        {
            var result = await _groupSubscriptionAppService.GetListAsync(new GetGroupSubscriptionListDto());
            GroupSubscriptions = result.Items;
        }
    }
}
