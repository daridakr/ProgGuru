using Daridakr.ProgGuru.Entities.Base;
using JetBrains.Annotations;
using System;

namespace Daridakr.ProgGuru.Entities.Users.Skills
{
    /// <summary>
    /// Common user skill.
    /// </summary>
    public class UserCommonSkill : UserSkillEntity
    {
        /* CreatorId, Name */

        /******* Initialization ********************************************************************************/

        /// <summary>
        /// This constructor is used by the ORM/database provider.
        /// </summary>
        protected UserCommonSkill() : base() { }

        /// <summary>
        /// This constructor is used for creating new skill.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="creatorId"></param>
        /// <param name="name"></param>
        internal UserCommonSkill(
            [NotNull] Guid id,
            [NotNull] Guid creatorId,
            [NotNull] string name)
            : base(id, creatorId, name)
        {

        }
    }
}
