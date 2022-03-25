using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Daridakr.ProgGuru.Entities.Users.JobExperiences
{
    public interface IUserJobExperienceRepository : IRepository<UserJobExperience, Guid>
    {
        Task<UserJobExperience> FindByIdAsync(Guid id);

        Task<List<UserJobExperience>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null
        );
    }
}
