using Daridakr.ProgGuru.Consts;
using Daridakr.ProgGuru.Entities.Base;
using Daridakr.ProgGuru.Entities.Groups;
using Daridakr.ProgGuru.Entities.Groups.Exceptions;
using Daridakr.ProgGuru.Entities.Users.Exceptions;
using JetBrains.Annotations;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace Daridakr.ProgGuru.Entities.Users.Skills
{
    /// <summary>
    /// Domain service class, that uses for the correct creation of a professional skill, that meets all the requirements of business-logic.
    /// </summary>
    public class UserProfSkillManager : UserSkillManager
    {
        private readonly IGroupRepository _groupRepository;

        public UserProfSkillManager(
            IUserCommonSkillRepository commonSkillRepository,
            IUserLanguageSkillRepository languageSkillRepository,
            IUserProfSkillRepository profSkillRepository,
            IRepository<IdentityUser, Guid> userRepository,
            IGroupRepository groupRepository)
            : base(commonSkillRepository, languageSkillRepository, profSkillRepository, userRepository)
        {
            _groupRepository = groupRepository;
        }

        /// <summary>
        /// Creating new valid professional skill in a controlled way.
        /// </summary>
        /// <param name="creatorId"></param>
        /// <param name="groupId"></param>
        /// <param name="name"></param>
        /// <param name="beginningYear"></param>
        /// <param name="knownledgeLevel"></param>
        /// <param name="endYear"></param>
        /// <returns></returns>
        public async Task<UserProfSkill> CreateAsync(
            [NotNull] Guid creatorId,
            [NotNull] Guid groupId,
            [NotNull] string name,
            [NotNull] int beginningYear,
            [NotNull] int knownledgeLevel,
            [CanBeNull] int endYear = 0)
        {
            await TryCreateUserSkillEntityAsync(creatorId, name);

            await checkIfGroupIsExistsAsync(groupId);
            checkIfBeginningYearCorrect(beginningYear);
            checkIfKnownledgeLevelCorrect(knownledgeLevel);
            checkIfEndYearCorrect(beginningYear, endYear);

            return new UserProfSkill(
                GuidGenerator.Create(),
                creatorId,
                groupId,
                name,
                beginningYear,
                knownledgeLevel,
                endYear
            );
        }

        /// <summary>
        /// Checks if a group exists.
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        /// <exception cref="GroupDoesntExistsException"></exception>
        private async Task checkIfGroupIsExistsAsync([NotNull] Guid groupId)
        {
            Check.NotNull(groupId, nameof(groupId));
            var existingGroup = await _groupRepository.FindByIdAsync(groupId);
            if (existingGroup == null) throw new GroupDoesntExistsException(groupId);
        }

        /// <summary>
        /// Checks if a beginning year correct.
        /// </summary>
        /// <param name="beginningYear"></param>
        /// <exception cref="BeginningYearInvalidException"></exception>
        private void checkIfBeginningYearCorrect([NotNull] int beginningYear)
        {
            Check.NotNull(beginningYear, nameof(beginningYear));
            int nowYear = DateTime.Now.Year;
            int startYear = nowYear - 90;
            if (beginningYear < startYear || beginningYear > nowYear) throw new BeginningYearInvalidException(startYear, nowYear);
        }

        /// <summary>
        /// Checks if a knownledge level correct.
        /// </summary>
        /// <param name="knownledgeLevel"></param>
        /// <exception cref="KnownledgeLevelInvalidException"></exception>
        private void checkIfKnownledgeLevelCorrect([NotNull] int knownledgeLevel)
        {
            Check.NotNull(knownledgeLevel, nameof(knownledgeLevel));
            if (knownledgeLevel < UserConsts.MinKnownledgeLevel || knownledgeLevel > UserConsts.MaxKnownledgeLevel) throw new KnownledgeLevelInvalidException();
        }

        /// <summary>
        /// Checks if a end year correct.
        /// </summary>
        /// <param name="beginningYear"></param>
        /// <param name="endYear"></param>
        /// <exception cref="EndYearInvalidException"></exception>
        private void checkIfEndYearCorrect(
            [NotNull] int beginningYear,
            [CanBeNull] int endYear = 0)
        {
            if (endYear != 0)
            {
                if (endYear <= beginningYear || beginningYear > DateTime.Now.Year) throw new EndYearInvalidException();
            }
        }

        /// <summary>
        /// Updating professional skill in a controlled way.
        /// </summary>
        /// <param name="profSkill"></param>
        /// <param name="creatorId"></param>
        /// <param name="groupId"></param>
        /// <param name="name"></param>
        /// <param name="beginningYear"></param>
        /// <param name="knownledgeLevel"></param>
        /// <param name="endYear"></param>
        /// <returns></returns>
        public async Task<UserProfSkill> UpdateAsync(
            [NotNull] UserProfSkill profSkill,
            [NotNull] Guid creatorId,
            [NotNull] Guid groupId,
            [NotNull] string name,
            [NotNull] int beginningYear,
            [NotNull] int knownledgeLevel,
            [CanBeNull] int endYear = 0)
        {
            Check.NotNull(profSkill, nameof(profSkill));
            await TryUpdateUserSkillAsync(profSkill, creatorId, name);

            ChangeGroup(profSkill, groupId);
            ChangeBeginningYear(profSkill, beginningYear);
            ChangeKnownledgeLevel(profSkill, knownledgeLevel);
            ChangeEndYear(profSkill, profSkill.BeginningYear, endYear);

            return profSkill;
        }

        /// <summary>
        /// Set new group for professional skill.
        /// </summary>
        /// <param name="profSkill"></param>
        /// <param name="groupId"></param>
        public async void ChangeGroup(
            [NotNull] UserProfSkill profSkill,
            [NotNull] Guid groupId)
        {
            Check.NotNull(profSkill, nameof(profSkill));
            await checkIfGroupIsExistsAsync(groupId);
            profSkill.ChangeGroup(groupId);
        }

        /// <summary>
        /// Set new beginning year for professional skill.
        /// </summary>
        /// <param name="profSkill"></param>
        /// <param name="newBeginningYear"></param>
        public void ChangeBeginningYear(
            [NotNull] UserProfSkill profSkill,
            [NotNull] int newBeginningYear)
        {
            Check.NotNull(profSkill, nameof(profSkill));
            checkIfBeginningYearCorrect(newBeginningYear);
            profSkill.ChangeBeginningYear(newBeginningYear);
        }

        /// <summary>
        /// Set new knownledge level for professional skill.
        /// </summary>
        /// <param name="profSkill"></param>
        /// <param name="newKnownledgeLevel"></param>
        public void ChangeKnownledgeLevel(
            [NotNull] UserProfSkill profSkill,
            [NotNull] int newKnownledgeLevel)
        {
            Check.NotNull(profSkill, nameof(profSkill));
            checkIfKnownledgeLevelCorrect(newKnownledgeLevel);
            profSkill.ChangeKnownledgeLevel(newKnownledgeLevel);
        }

        /// <summary>
        /// Set new end year for professional skill.
        /// </summary>
        /// <param name="profSkill"></param>
        /// <param name="newEndYear"></param>
        /// <returns></returns>
        public void ChangeEndYear(
            [NotNull] UserProfSkill profSkill,
            [NotNull] int beginningYear,
            [CanBeNull] int newEndYear)
        {
            Check.NotNull(profSkill, nameof(profSkill));
            checkIfEndYearCorrect(beginningYear, newEndYear);
            profSkill.ChangeEndYear(newEndYear);
        }
    }
}
