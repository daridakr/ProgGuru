using Daridakr.ProgGuru.Entities.Base;
using Daridakr.ProgGuru.Enums.Users;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace Daridakr.ProgGuru.Entities.Users.Skills
{
    /// <summary>
    /// Domain service class, that uses for the correct creation of a language skill, that meets all the requirements of business-logic.
    /// </summary>
    public class UserLanguageSkillManager : UserSkillManager
    {
        public UserLanguageSkillManager(
            IUserCommonSkillRepository commonSkillRepository,
            IUserLanguageSkillRepository languageSkillRepository,
            IUserProfSkillRepository profSkillRepository,
            IRepository<IdentityUser, Guid> userRepository)
            : base(commonSkillRepository, languageSkillRepository, profSkillRepository, userRepository)
        {
            
        }

        /// <summary>
        /// Creating new valid language skill in a controlled way.
        /// </summary>
        /// <param name="creatorId"></param>
        /// <param name="name"></param>
        /// <param name="languageLevel"></param>
        /// <returns></returns>
        public async Task<UserLanguageSkill> CreateAsync(
            [NotNull] Guid creatorId,
            [NotNull] string name,
            [NotNull] UserLanguageLevel languageLevel)
        {
            await TryCreateUserSkillEntityAsync(creatorId, name);
            Check.NotNull(languageLevel, nameof(languageLevel));

            return new UserLanguageSkill(
                GuidGenerator.Create(),
                creatorId,
                name,
                languageLevel
            );
        }

        /// <summary>
        /// Updating language skill in a controlled way.
        /// </summary>
        /// <param name="languageSkill"></param>
        /// <param name="creatorId"></param>
        /// <param name="name"></param>
        /// <param name="languageLevel"></param>
        /// <returns></returns>
        public async Task<UserLanguageSkill> UpdateAsync(
            [NotNull] UserLanguageSkill languageSkill,
            [NotNull] Guid creatorId,
            [NotNull] string name,
            [NotNull] UserLanguageLevel languageLevel)
        {
            Check.NotNull(languageSkill, nameof(languageSkill));
            await TryUpdateUserSkillAsync(languageSkill, creatorId, name);
            ChangeLanguageLevel(languageSkill, languageLevel);

            return languageSkill;
        }

        /// <summary>
        /// Set new language level for language skill.
        /// </summary>
        /// <param name="languageSkill"></param>
        /// <param name="newLanguageLevel"></param>
        public void ChangeLanguageLevel(
            [NotNull] UserLanguageSkill languageSkill,
            [NotNull] UserLanguageLevel newLanguageLevel)
        {
            Check.NotNull(languageSkill, nameof(languageSkill));
            Check.NotNull(newLanguageLevel, nameof(newLanguageLevel));
            languageSkill.ChangeLanguageLevel(newLanguageLevel);
        }
    }
}
