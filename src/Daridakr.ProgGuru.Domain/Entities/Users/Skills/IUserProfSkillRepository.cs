using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Daridakr.ProgGuru.Entities.Users.Skills
{
    public interface IUserProfSkillRepository : IRepository<UserProfSkill, Guid>
    {
        Task<UserProfSkill> FindByIdAsync(Guid id);

        Task<UserProfSkill> FindByNameAsync(Guid userId, string name);

        Task<List<UserProfSkill>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null
        );
    }
}
