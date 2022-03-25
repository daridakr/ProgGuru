using Daridakr.ProgGuru.Entities.Users.JobExperiences;
using Daridakr.ProgGuru.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Daridakr.ProgGuru.Repositories
{
    public class EfCoreUserJobExperienceRepository
        : EfCoreRepository<ProgGuruDbContext, UserJobExperience, Guid>,
            IUserJobExperienceRepository
    {
        public EfCoreUserJobExperienceRepository(
            IDbContextProvider<ProgGuruDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<UserJobExperience> FindByIdAsync(Guid id)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.FirstOrDefaultAsync(jobExperience => jobExperience.Id == id);
        }

        public async Task<List<UserJobExperience>> GetListAsync(int skipCount, int maxResultCount, string sorting, string filter = null)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    jobExperience => jobExperience.Position.Contains(filter)
                 )
                .OrderBy(sorting)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }
    }
}
