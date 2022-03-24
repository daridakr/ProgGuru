using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Daridakr.ProgGuru.Users.JobExperiences
{
    public interface IJobExperienceAppService : IApplicationService
    {
        Task<JobExperienceDto> GetAsync(Guid id);

        Task<List<JobExperienceDto>> GetListAsync();

        Task<JobExperienceDto> CreateAsync(CreateJobExperienceDto input);

        Task<JobExperienceDto> UpdateAsync(Guid id, UpdateJobExperienceDto input);

        Task DeleteAsync(Guid id);
    }
}
