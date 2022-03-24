using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Daridakr.ProgGuru.Entities.Groups;
using Daridakr.ProgGuru.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Daridakr.ProgGuru.Repositories
{
    public class EfCoreGroupRepository
        : EfCoreRepository<ProgGuruDbContext, Group, Guid>,
            IGroupRepository
    {
        public EfCoreGroupRepository(
            IDbContextProvider<ProgGuruDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<Group> FindByIdAsync(Guid id)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.FirstOrDefaultAsync(group => group.Id == id);
        }

        public async Task<Group> FindByTitleAsync(string title)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.FirstOrDefaultAsync(group => group.Title == title);
        }

        public async Task<List<Group>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null)
        {
            var dbSet = await GetDbSetAsync();
            // It adds the Where condition only if the first condition meets (it filters by title, only if the filter was provided)
            return await dbSet
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    group => group.Title.Contains(filter)
                 )
                .OrderBy(sorting)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }
    }
}
