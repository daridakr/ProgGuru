using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Daridakr.ProgGuru.Entities.Users.Skills
{
    public interface IUserLanguageSkillRepository : IRepository<UserLanguageSkill, Guid>
    {
        Task<UserLanguageSkill> FindByIdAsync(Guid id);

        Task<UserLanguageSkill> FindByNameAsync(Guid userId, string name);

        Task<List<UserLanguageSkill>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null
        );
    }
}
