using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Daridakr.ProgGuru.Entities.Users.Specializations;
using Daridakr.ProgGuru.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Daridakr.ProgGuru.Repositories
{
    public class EfCoreSpecializationRepository
        : EfCoreRepository<ProgGuruDbContext, Specialization, Guid>,
            ISpecializationRepository
    {
        public EfCoreSpecializationRepository(
            IDbContextProvider<ProgGuruDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<Specialization> FindByNameAsync(string name)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.FirstOrDefaultAsync(special => special.Name == name);
        }

        public async Task<List<Specialization>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    author => author.Name.Contains(filter)
                 )
                .OrderBy(sorting)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }
    }
}
