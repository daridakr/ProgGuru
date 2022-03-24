using Daridakr.ProgGuru.Entities.Users.Exceptions;
using Daridakr.ProgGuru.Entities.Users.Skills;
using JetBrains.Annotations;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Identity;

namespace Daridakr.ProgGuru.Entities.Base
{
    public class UserSkillManager : DomainService
    {
        private readonly IUserCommonSkillRepository _commonSkillRepository;
        private readonly IUserLanguageSkillRepository _languageSkillRepository;
        private readonly IUserProfSkillRepository _profSkillRepository;

        private readonly IRepository<IdentityUser, Guid> _userRepository;

        public UserSkillManager(
            IUserCommonSkillRepository commonSkillRepository,
            IUserLanguageSkillRepository languageSkillRepository,
            IUserProfSkillRepository profSkillRepository,
            IRepository<IdentityUser, Guid> userRepository)
        {
            _commonSkillRepository = commonSkillRepository;
            _languageSkillRepository = languageSkillRepository;
            _profSkillRepository = profSkillRepository;
            _userRepository = userRepository;
        }

        protected async Task TryCreateUserSkillEntityAsync(
            [NotNull] Guid creatorId,
            [NotNull] string name)
        {
            await checkIfSkillIsExistsAsync(creatorId, name);
            await checkIfCreatorExistsAsync(creatorId);
        }

        /// <summary>
        /// Checks if a skill exists.
        /// </summary>
        /// <param name="creatorId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="SkillAlreadyExistsException"></exception>
        protected async Task checkIfSkillIsExistsAsync([NotNull] Guid creatorId, [NotNull] string name)
        {
            var existingCommonSkill = await _commonSkillRepository.FindByNameAsync(creatorId, name);
            var existingLanguageSkill = await _languageSkillRepository.FindByNameAsync(creatorId, name);
            var existingProfSkill = await _profSkillRepository.FindByNameAsync(creatorId, name);
            if (existingCommonSkill != null || existingLanguageSkill != null || existingProfSkill != null) throw new SkillAlreadyExistsException(name);
        }

        /// <summary>
        /// Checks if a user exists.
        /// </summary>
        /// <param name="creatorId"></param>
        /// <returns></returns>
        protected async Task checkIfCreatorExistsAsync([NotNull] Guid creatorId)
        {
            Check.NotNull(creatorId, nameof(creatorId));
            var existingUser = await _userRepository.FindAsync(creatorId);
            if (existingUser == null) throw new UserDoesntExistsException(creatorId);
        }

        protected async Task TryUpdateUserSkillAsync(
            [NotNull] UserSkillEntity skill,
            [NotNull] Guid creatorId,
            [NotNull] string name)
        {
            Check.NotNull(skill, nameof(skill));
            await ChangeNameAsync(skill, creatorId, name);
        }

        /// <summary>
        /// Set new name for skill.
        /// </summary>
        /// <param name="skill"></param>
        /// <param name="newName"></param>
        /// <returns></returns>
        /// <exception cref="SkillAlreadyExistsException"></exception>
        public async Task ChangeNameAsync(
            [NotNull] UserSkillEntity skill,
            [NotNull] Guid creatorId,
            [NotNull] string newName)
        {
            Check.NotNull(skill, nameof(skill));
            Check.NotNullOrWhiteSpace(newName, nameof(newName));

            var existingCommonSkill = await _commonSkillRepository.FindByNameAsync(creatorId, newName);
            var existingLanguageSkill = await _languageSkillRepository.FindByNameAsync(creatorId, newName);
            var existingProfSkill = await _profSkillRepository.FindByNameAsync(creatorId, newName);

            if ((existingCommonSkill != null && existingCommonSkill.Id != skill.Id) ||
                (existingLanguageSkill != null && existingLanguageSkill.Id != skill.Id) ||
                (existingProfSkill != null && existingProfSkill.Id != skill.Id))
            {
                throw new SkillAlreadyExistsException(newName);
            }

            skill.ChangeName(newName);
        }
    }
}
