using Daridakr.ProgGuru.Entities.Groups;
using Daridakr.ProgGuru.Entities.Users.Skills;
using Daridakr.ProgGuru.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Daridakr.ProgGuru.Users.Skills
{
    public class ProfSkillAppService : ProgGuruAppService, IProfSkillAppService
    {
        private readonly IUserProfSkillRepository _profSkillRepository;
        private readonly IGroupRepository _groupRepository;

        private readonly UserProfSkillManager _profSkillManager;

        public ProfSkillAppService(
            IUserProfSkillRepository profSkillRepository,
            IGroupRepository groupRepository,
            UserProfSkillManager profSkillManager)
        {
            _profSkillRepository = profSkillRepository;
            _groupRepository = groupRepository;
            _profSkillManager = profSkillManager;
        }

        public async Task<ProfSkillDto> GetAsync(Guid id)
        {
            var profSkill = await _profSkillRepository.GetAsync(id);
            return ObjectMapper.Map<UserProfSkill, ProfSkillDto>(profSkill);
        }

        public async Task<List<ProfSkillDto>> GetListAsync()
        {
            var profSkills = await _profSkillRepository.GetListAsync();
            var query = from profSkill in profSkills
                        join @group in await _groupRepository.GetQueryableAsync() on profSkill.GroupId equals @group.Id
                        select new { profSkill, @group };

            var profSkillDtos = query.Select(x =>
            {
                var profSkillDto = ObjectMapper.Map<UserProfSkill, ProfSkillDto>(x.profSkill);
                profSkillDto.Name = x.group.Title;
                profSkillDto.GroupCoverImagePath = x.group.CoverImagePath;
                return profSkillDto;
            }).ToList();

            return profSkillDtos;
        }

        public async Task<ProfSkillDto> CreateAsync(CreateProfSkillDto input)
        {
            var group = await _groupRepository.GetAsync(input.GroupId);
            var profSkill = await _profSkillManager.CreateAsync(
                input.CreatorId,
                input.GroupId,
                group.Title,
                input.BeginningYear,
                input.KnownledgeLevel,
                input.EndYear
                );

            await _profSkillRepository.InsertAsync(profSkill);

            return ObjectMapper.Map<UserProfSkill, ProfSkillDto>(profSkill);
        }

        public async Task<ProfSkillDto> UpdateAsync(Guid id, UpdateProfSkillDto input)
        {
            var profSkill = await _profSkillRepository.GetAsync(id);
            var group = await _groupRepository.GetAsync(input.GroupId);

            profSkill = await _profSkillManager.UpdateAsync(
                profSkill,
                (Guid)profSkill.CreatorId,
                input.GroupId,
                group.Title,
                input.BeginningYear,
                input.KnownledgeLevel,
                input.EndYear
                );

            return ObjectMapper.Map<UserProfSkill, ProfSkillDto>(profSkill);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _profSkillRepository.DeleteAsync(id);
        }

        public async Task<ListResultDto<GroupLookupDto>> GetGroupLookupAsync()
        {
            var groups = await _groupRepository.GetListAsync();

            return new ListResultDto<GroupLookupDto>(
                ObjectMapper.Map<List<Group>, List<GroupLookupDto>>(groups)
            );
        }
    }
}
