using Daridakr.ProgGuru.Entities.Users.JobExperiences;
using Daridakr.ProgGuru.Users.JobExperiences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Daridakr.ProgGuru.Users
{
    public class JobExperienceAppService : ProgGuruAppService, IJobExperienceAppService
    {
        private readonly IUserJobExperienceRepository _jobExperienceRepository;
        private readonly UserJobExperienceManager _jobExperienceManager;

        public JobExperienceAppService(
            IUserJobExperienceRepository jobExperienceRepository,
            UserJobExperienceManager jobExperienceManager)
        {
            _jobExperienceRepository = jobExperienceRepository;
            _jobExperienceManager = jobExperienceManager;
        }

        public async Task<JobExperienceDto> GetAsync(Guid id)
        {
            var jobExperience = await _jobExperienceRepository.GetAsync(id);
            return ObjectMapper.Map<UserJobExperience, JobExperienceDto>(jobExperience);
        }

        public async Task<List<JobExperienceDto>> GetListAsync()
        {
            var jobExperiences = await _jobExperienceRepository.GetListAsync();
            return jobExperiences
                .Select(item => new JobExperienceDto
                {
                    Id = item.Id,
                    CreatorId = item.CreatorId,
                    Position = item.Position,
                    CompanyName = item.CompanyName,
                    BeginningDate = item.BeginningDate,
                    CreationTime = item.CreationTime,
                    DeleterId = item.DeleterId,
                    DeletionTime = item.DeletionTime,
                    EndDate = item.EndDate,
                    IsCurrent = item.IsCurrent,
                    IsDeleted = item.IsDeleted,
                    LastModificationTime = item.LastModificationTime,
                    LastModifierId = item.LastModifierId,
                    Location = item.Location,
                    PositionCategory = item.PositionCategory
                }).ToList();
        }

        public async Task<JobExperienceDto> CreateAsync(CreateJobExperienceDto input)
        {
            var jobExperience = _jobExperienceManager.CreateAsync(
                input.CreatorId,
                input.Position,
                input.CompanyName,
                input.PositionCategory,
                input.Location,
                input.BeginningDate,
                input.EndDate
                );

            await _jobExperienceRepository.InsertAsync(jobExperience);

            return ObjectMapper.Map<UserJobExperience, JobExperienceDto>(jobExperience);
        }

        public async Task<JobExperienceDto> UpdateAsync(Guid id, UpdateJobExperienceDto input)
        {
            var jobExperience = await _jobExperienceRepository.GetAsync(id);

            jobExperience = _jobExperienceManager.UpdateAsync(
                jobExperience,
                input.Position,
                input.CompanyName,
                input.PositionCategory,
                input.Location,
                input.BeginningDate,
                input.EndDate
                );

            return ObjectMapper.Map<UserJobExperience, JobExperienceDto>(jobExperience);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _jobExperienceRepository.DeleteAsync(id);
        }
    }
}
