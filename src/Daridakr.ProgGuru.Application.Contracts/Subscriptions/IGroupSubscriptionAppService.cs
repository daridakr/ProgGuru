using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Daridakr.ProgGuru.Subscriptions
{
    public interface IGroupSubscriptionAppService : IApplicationService
    {
        Task<List<GroupSubscriberDto>> GetGroupSubscribersAsync();

        Task<List<UserGroupSubscriptionDto>> GetUserSubscriptionsAsync();

        Task<PagedResultDto<AdminGroupSubscriptionDto>> GetListAsync(GetGroupSubscriptionListDto input);

        Task<bool> TrySubscribeAsync(Guid creatorId, Guid groupId);

        Task SubscribeAsync(Guid creatorId, Guid groupId);

        Task UnsubscribeAsync(Guid creatorId, Guid groupId);
    }
}
