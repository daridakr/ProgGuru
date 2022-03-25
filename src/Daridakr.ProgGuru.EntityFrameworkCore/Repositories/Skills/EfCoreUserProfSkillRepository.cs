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
    public class EfCoreUserProfSkillRepository
        : EfCoreRepository<ProgGuruDbContext, UserProfSkill, Guid>,
            IUserProfSkillRepository
    {
        public EfCoreUserProfSkillRepository(
            IDbContextProvider<ProgGuruDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<UserProfSkill> FindByIdAsync(Guid id)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.FirstOrDefaultAsync(profSkill => profSkill.Id == id);
        }

        public async Task<UserProfSkill> FindByNameAsync(Guid userId, string name)
        {
            var dbSet = await GetDbSetAsync();
            var userSkills = dbSet.Where(profSkill => profSkill.CreatorId == userId);
            return await userSkills.Where(profSkill => profSkill.Name == name).FirstOrDefaultAsync();
        }

        public async Task<List<UserProfSkill>> GetListAsync(int skipCount, int maxResultCount, string sorting, string filter = null)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    profSkill => profSkill.Name.Contains(filter)
                 )
                .OrderBy(sorting)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }
    }
}
