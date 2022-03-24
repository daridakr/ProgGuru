using Daridakr.ProgGuru.Subscriptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Daridakr.ProgGuru.Web.Pages.Subscriptions
{
    public class UserSubscriptionIndexModel : ProgGuruPageModel
    {
        public IReadOnlyList<AdminUserSubscriptionDto> UserSubscriptions { get; set; }

        private readonly IUserSubscriptionAppService _userSubscriptionAppService;

        public UserSubscriptionIndexModel(IUserSubscriptionAppService userSubscriptionAppService)
        {
            _userSubscriptionAppService = userSubscriptionAppService;
        }

        public async Task OnGetAsync()
        {
            var result = await _userSubscriptionAppService.GetListAsync(new GetUserSubscriptionListDto());
            UserSubscriptions = result.Items;
        }
    }
}
