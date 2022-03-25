using Daridakr.ProgGuru.Entities.Users.Skills;
using Daridakr.ProgGuru.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Daridakr.ProgGuru.Repositories.Skills
{
    public class EfCoreUserCommonSkillRepository
        : EfCoreRepository<ProgGuruDbContext, UserCommonSkill, Guid>,
            IUserCommonSkillRepository
    {
        public EfCoreUserCommonSkillRepository(
            IDbContextProvider<ProgGuruDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<UserCommonSkill> FindByIdAsync(Guid id)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.FirstOrDefaultAsync(commonSkill => commonSkill.Id == id);
        }

        public async Task<UserCommonSkill> FindByNameAsync(Guid userId, string name)
        {
            var dbSet = await GetDbSetAsync();
            var userSkills = dbSet.Where(commonSkill => commonSkill.CreatorId == userId);
            return await userSkills.Where(commonSkill => commonSkill.Name == name).FirstOrDefaultAsync();
        }

        public async Task<List<UserCommonSkill>> GetListAsync(int skipCount, int maxResultCount, string sorting, string filter = null)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    commonSkill => commonSkill.Name.Contains(filter)
                 )
                .OrderBy(sorting)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }
    }
}
