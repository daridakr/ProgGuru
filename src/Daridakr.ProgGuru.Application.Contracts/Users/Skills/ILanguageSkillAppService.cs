using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Daridakr.ProgGuru.Users.Skills
{
    public interface ILanguageSkillAppService : IApplicationService
    {
        Task<LanguageSkillDto> GetAsync(Guid id);

        Task<List<LanguageSkillDto>> GetListAsync();

        Task<LanguageSkillDto> CreateAsync(CreateLanguageSkillDto input);

        Task<LanguageSkillDto> UpdateAsync(Guid id, UpdateLanguageSkillDto input);

        Task DeleteAsync(Guid id);
    }
}
