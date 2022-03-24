using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Daridakr.ProgGuru.Users.Skills
{
    public interface ICommonSkillAppService : IApplicationService
    {
        Task<List<CommonSkillDto>> GetListAsync();

        Task<CommonSkillDto> CreateAsync(CreateCommonSkillDto input);

        Task<CommonSkillDto> UpdateAsync(Guid id, UpdateCommonSkillDto input);

        Task DeleteAsync(Guid id);
    }
}
