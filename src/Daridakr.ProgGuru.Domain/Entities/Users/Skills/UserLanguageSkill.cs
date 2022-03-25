using Daridakr.ProgGuru.Entities.Base;
using Daridakr.ProgGuru.Enums.Users;
using JetBrains.Annotations;
using System;
using Volo.Abp;

namespace Daridakr.ProgGuru.Entities.Users.Skills
{
    /// <summary>
    /// Language user skill.
    /// </summary>
    public class UserLanguageSkill : UserSkillEntity
    {
        /* CreatorId, Name */

        public UserLanguageLevel LanguageLevel { get; private set; }

        /******* Initialization ********************************************************************************/

        /// <summary>
        /// This constructor is used by the ORM/database provider.
        /// </summary>
        protected UserLanguageSkill() : base() { }

        /// <summary>
        /// This constructor is used for creating new language skill.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="creatorId"></param>
        /// <param name="name"></param>
        /// <param name="languageLevel"></param>
        internal UserLanguageSkill(
            [NotNull] Guid id,
            [NotNull] Guid creatorId,
            [NotNull] string name,
            [NotNull] UserLanguageLevel languageLevel)
            : base(id, creatorId, name)
        {
            SetLanguageLevel(languageLevel);
        }

        /// <summary>
        /// Correct language level initialize.
        /// </summary>
        /// <param name="languageLevel"></param>
        protected void SetLanguageLevel([NotNull] UserLanguageLevel languageLevel)
        {
            LanguageLevel = Check.NotNull(languageLevel, nameof(languageLevel));
        }

        /// <summary>
        /// Correct language level changing.
        /// </summary>
        /// <param name="languageLevel"></param>
        /// <returns></returns>
        internal UserLanguageSkill ChangeLanguageLevel([NotNull] UserLanguageLevel languageLevel)
        {
            SetLanguageLevel(languageLevel);
            return this;
        }
    }
}
