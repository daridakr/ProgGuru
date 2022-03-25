using Daridakr.ProgGuru.Projects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Daridakr.ProgGuru.Users.Skills
{
    public interface IProfSkillAppService : IApplicationService
    {
        Task<ProfSkillDto> GetAsync(Guid id);

        Task<List<ProfSkillDto>> GetListAsync();

        Task<ProfSkillDto> CreateAsync(CreateProfSkillDto input);

        Task<ProfSkillDto> UpdateAsync(Guid id, UpdateProfSkillDto input);

        Task DeleteAsync(Guid id);

        Task<ListResultDto<GroupLookupDto>> GetGroupLookupAsync();
    }
}
