using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Daridakr.ProgGuru.Subscriptions
{
    public interface IUserSubscriptionAppService : IApplicationService
    {
        Task<List<UserSubscriptionDto>> GetUserSubscribersAsync();

        Task<List<UserSubscriptionDto>> GetUserSubscriptionsAsync();

        Task<PagedResultDto<AdminUserSubscriptionDto>> GetListAsync(GetUserSubscriptionListDto input);

        Task<bool> TrySubscribeAsync(Guid creatorId, Guid userId);

        Task SubscribeAsync(Guid creatorId, Guid userId);

        Task UnsubscribeAsync(Guid creatorId, Guid userId);
    }
}
