using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Daridakr.ProgGuru.Entities.Subscriptions
{
    public interface IGroupSubscriptionRepository : IRepository<GroupSubscription, Guid>
    {
        Task<List<GroupSubscription>> GetListAsync();

        Task<List<GroupSubscription>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting);
    }
}
