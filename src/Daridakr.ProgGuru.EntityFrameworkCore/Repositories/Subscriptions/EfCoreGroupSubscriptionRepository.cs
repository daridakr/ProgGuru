using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Daridakr.ProgGuru.Entities.Subscriptions;
using Daridakr.ProgGuru.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Daridakr.ProgGuru.Repositories.Subscriptions
{
    public class EfCoreGroupSubscriptionRepository
        : EfCoreRepository<ProgGuruDbContext, GroupSubscription, Guid>,
            IGroupSubscriptionRepository
    {
        public EfCoreGroupSubscriptionRepository(
            IDbContextProvider<ProgGuruDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<GroupSubscription>> GetListAsync()
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.ToListAsync();
        }

        public async Task<List<GroupSubscription>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .OrderBy(sorting)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }
    }
}
