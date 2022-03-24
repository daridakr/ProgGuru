using Daridakr.ProgGuru.Entities.Users.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Daridakr.ProgGuru.Users.Skills
{
    public class CommonSkillAppService : ProgGuruAppService, ICommonSkillAppService
    {
        private readonly IUserCommonSkillRepository _commonSkillRepository;
        private readonly UserCommonSkillManager _commonSkillManager;

        public CommonSkillAppService(
            IUserCommonSkillRepository commonSkillRepository,
            UserCommonSkillManager commonSkillManager)
        {
            _commonSkillRepository = commonSkillRepository;
            _commonSkillManager = commonSkillManager;
        }

        public async Task<List<CommonSkillDto>> GetListAsync()
        {
            var commonSkills = await _commonSkillRepository.GetListAsync();
            return commonSkills
                .Select(item => new CommonSkillDto
                {
                    Id = item.Id,
                    CreatorId = item.CreatorId,
                    Name = item.Name
                }).ToList();
        }

        public async Task<CommonSkillDto> CreateAsync(CreateCommonSkillDto input)
        {
            var commonSkill = await _commonSkillManager.CreateAsync(
                input.CreatorId,
                input.Name
                );

            await _commonSkillRepository.InsertAsync(commonSkill);

            return ObjectMapper.Map<UserCommonSkill, CommonSkillDto>(commonSkill);
        }

        public async Task<CommonSkillDto> UpdateAsync(Guid id, UpdateCommonSkillDto input)
        {
            var commonSkill = await _commonSkillRepository.GetAsync(id);

            commonSkill = await _commonSkillManager.UpdateAsync(
                commonSkill,
                (Guid)commonSkill.CreatorId,
                input.Name
                );

            return ObjectMapper.Map<UserCommonSkill, CommonSkillDto>(commonSkill);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _commonSkillRepository.DeleteAsync(id);
        }
    }
}
