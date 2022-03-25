using Daridakr.ProgGuru.Consts;
using Daridakr.ProgGuru.Helpers;
using JetBrains.Annotations;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Daridakr.ProgGuru.Entities.Base
{
    /// <summary>
    /// Abstract base class for user skill entities.
    /// </summary>
    public abstract class UserSkillEntity : AuditedAggregateRoot<Guid>
    {
        public string Name { get; private set; }

        /******* Initialization ********************************************************************************/

        /// <summary>
        /// This constructor is used by the ORM/database provider.
        /// </summary>
        protected UserSkillEntity() { }

        /// <summary>
        /// This constructor is used for creating new skill.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="creatorId"></param>
        /// <param name="name"></param>
        internal UserSkillEntity(
            [NotNull] Guid id,
            [NotNull] Guid creatorId,
            [NotNull] string name)
            : base(id)
        {
            SetCreator(creatorId);
            SetName(name);
        }

        /// <summary>
        /// Correct skill creator initialize.
        /// </summary>
        /// <param name="creatorId"></param>
        protected void SetCreator([NotNull] Guid creatorId)
        {
            CreatorId = Check.NotNull(creatorId, nameof(creatorId));
        }

        /// <summary>
        /// Correct skill name initialize.
        /// </summary>
        /// <param name="name"></param>
        protected void SetName([NotNull] string name)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name),
                minLength: UserConsts.MinSkillNameLength,
                maxLength: UserConsts.MaxSkillNameLength);
            Name = ProgGuruCheck.IsStartsWithSeparatorChar(name, nameof(name));
        }

        /// <summary>
        /// Correct skill name changing.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        internal UserSkillEntity ChangeName([NotNull] string name)
        {
            SetName(name);
            return this;
        }
    }
}
