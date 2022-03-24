using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Daridakr.ProgGuru.Entities.Users.Skills
{
    public interface IUserCommonSkillRepository : IRepository<UserCommonSkill, Guid>
    {
        Task<UserCommonSkill> FindByIdAsync(Guid id);

        Task<UserCommonSkill> FindByNameAsync(Guid userId, string name);

        Task<List<UserCommonSkill>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null
        );
    }
}
