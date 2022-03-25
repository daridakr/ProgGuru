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
    public class EfCoreUserLanguageSkillRepository
        : EfCoreRepository<ProgGuruDbContext, UserLanguageSkill, Guid>,
            IUserLanguageSkillRepository
    {
        public EfCoreUserLanguageSkillRepository(
            IDbContextProvider<ProgGuruDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<UserLanguageSkill> FindByIdAsync(Guid id)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.FirstOrDefaultAsync(languageSkill => languageSkill.Id == id);
        }

        public async Task<UserLanguageSkill> FindByNameAsync(Guid userId, string name)
        {
            var dbSet = await GetDbSetAsync();
            var userSkills = dbSet.Where(languageSkill => languageSkill.CreatorId == userId);
            return await userSkills.Where(languageSkill => languageSkill.Name == name).FirstOrDefaultAsync();
        }

        public async Task<List<UserLanguageSkill>> GetListAsync(int skipCount, int maxResultCount, string sorting, string filter = null)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    languageSkill => languageSkill.Name.Contains(filter)
                 )
                .OrderBy(sorting)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }
    }
}
