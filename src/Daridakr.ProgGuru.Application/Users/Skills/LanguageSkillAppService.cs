using Daridakr.ProgGuru.Entities.Users.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Daridakr.ProgGuru.Users.Skills
{
    public class LanguageSkillAppService : ProgGuruAppService, ILanguageSkillAppService
    {
        private readonly IUserLanguageSkillRepository _languageSkillRepository;
        private readonly UserLanguageSkillManager _languageSkillManager;

        public LanguageSkillAppService(
            IUserLanguageSkillRepository languageSkillRepository,
            UserLanguageSkillManager languageSkillManager)
        {
            _languageSkillRepository = languageSkillRepository;
            _languageSkillManager = languageSkillManager;
        }

        public async Task<LanguageSkillDto> GetAsync(Guid id)
        {
            var languageSkill = await _languageSkillRepository.GetAsync(id);
            return ObjectMapper.Map<UserLanguageSkill, LanguageSkillDto>(languageSkill);
        }

        public async Task<List<LanguageSkillDto>> GetListAsync()
        {
            var languageSkills = await _languageSkillRepository.GetListAsync();
            return languageSkills
                .Select(item => new LanguageSkillDto
                {
                    Id = item.Id,
                    CreatorId = item.CreatorId,
                    Name = item.Name,
                    LanguageLevel = item.LanguageLevel
                }).ToList();
        }

        public async Task<LanguageSkillDto> CreateAsync(CreateLanguageSkillDto input)
        {
            var languageSkill = await _languageSkillManager.CreateAsync(
                input.CreatorId,
                input.Name,
                input.LanguageLevel
                );

            await _languageSkillRepository.InsertAsync(languageSkill);

            return ObjectMapper.Map<UserLanguageSkill, LanguageSkillDto>(languageSkill);
        }

        public async Task<LanguageSkillDto> UpdateAsync(Guid id, UpdateLanguageSkillDto input)
        {
            var languageSkill = await _languageSkillRepository.GetAsync(id);

            languageSkill = await _languageSkillManager.UpdateAsync(
                languageSkill,
                (Guid)languageSkill.CreatorId,
                input.Name,
                input.LanguageLevel
                );

            return ObjectMapper.Map<UserLanguageSkill, LanguageSkillDto>(languageSkill);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _languageSkillRepository.DeleteAsync(id);
        }
    }
}
