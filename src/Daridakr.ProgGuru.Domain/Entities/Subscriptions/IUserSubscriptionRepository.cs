using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Daridakr.ProgGuru.Entities.Subscriptions
{
    public interface IUserSubscriptionRepository : IRepository<UserSubscription, Guid>
    {
        Task<List<UserSubscription>> GetListAsync();

        Task<List<UserSubscription>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting);
    }
}
