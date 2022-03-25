using Daridakr.ProgGuru.Entities.Base;
using JetBrains.Annotations;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace Daridakr.ProgGuru.Entities.Users.Skills
{
    /// <summary>
    /// Domain service class, that uses for the correct creation of a common skill, that meets all the requirements of business-logic.
    /// </summary>
    public class UserCommonSkillManager : UserSkillManager
    {
        public UserCommonSkillManager(
            IUserCommonSkillRepository commonSkillRepository,
            IUserLanguageSkillRepository languageSkillRepository,
            IUserProfSkillRepository profSkillRepository,
            IRepository<IdentityUser, Guid> userRepository)
            : base(commonSkillRepository, languageSkillRepository, profSkillRepository, userRepository)
        {

        }

        /// <summary>
        /// Creating new valid common skill in a controlled way.
        /// </summary>
        /// <param name="creatorId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<UserCommonSkill> CreateAsync(
            [NotNull] Guid creatorId,
            [NotNull] string name)
        {
            await TryCreateUserSkillEntityAsync(creatorId, name);
            return new UserCommonSkill(
                GuidGenerator.Create(),
                creatorId,
                name
            );
        }

        /// <summary>
        /// Updating common skill in a controlled way.
        /// </summary>
        /// <param name="commonSkill"></param>
        /// <param name="creatorId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<UserCommonSkill> UpdateAsync(
            [NotNull] UserCommonSkill commonSkill,
            [NotNull] Guid creatorId,
            [NotNull] string name)
        {
            Check.NotNull(commonSkill, nameof(commonSkill));
            await TryUpdateUserSkillAsync(commonSkill, creatorId, name);
            await ChangeNameAsync(commonSkill, creatorId, name);

            return commonSkill;
        }
    }
}
