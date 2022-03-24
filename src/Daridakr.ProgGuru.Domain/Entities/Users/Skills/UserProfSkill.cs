using Daridakr.ProgGuru.Entities.Base;
using JetBrains.Annotations;
using System;
using Volo.Abp;

namespace Daridakr.ProgGuru.Entities.Users.Skills
{
    /// <summary>
    /// Professional user skill.
    /// </summary>
    public class UserProfSkill : UserSkillEntity
    {
        /* CreatorId, Name */

        /// <summary>
        /// Skill related group.
        /// </summary>
        public Guid GroupId { get; private set; }

        /// <summary>
        /// The year the user started learning the skill.
        /// </summary>
        public int BeginningYear { get; private set; }

        /// <summary>
        /// The year the user stopped learning the skill.
        /// </summary>
        public int EndYear { get; private set; }

        /// <summary>
        /// Skill level one to five.
        /// </summary>
        public int KnownledgeLevel { get; private set; }

        /******* Initialization ********************************************************************************/

        /// <summary>
        /// This constructor is used by the ORM/database provider.
        /// </summary>
        protected UserProfSkill() : base() { }

        /// <summary>
        /// This constructor is used for creating new professional skill.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="creatorId"></param>
        /// <param name="groupId"></param>
        /// <param name="name"></param>
        /// <param name="beginningYear"></param>
        /// <param name="knownledgeLevel"></param>
        /// <param name="endYear"></param>
        internal UserProfSkill(
            [NotNull] Guid id,
            [NotNull] Guid creatorId,
            [NotNull] Guid groupId,
            [NotNull] string name,
            [NotNull] int beginningYear,
            [NotNull] int knownledgeLevel,
            [CanBeNull] int endYear = 0)
            : base(id, creatorId, name)
        {
            SetGroup(groupId);
            SetBeginningYear(beginningYear);
            SetKnownledgeLevel(knownledgeLevel);
            EndYear = endYear;
        }

        /// <summary>
        /// Correct skill group initialize.
        /// </summary>
        /// <param name="groupId"></param>
        protected void SetGroup([NotNull] Guid groupId)
        {
            GroupId = Check.NotNull(groupId, nameof(groupId));
        }

        /// <summary>
        /// Correct skill beginning year initialize.
        /// </summary>
        /// <param name="beginningYear"></param>
        protected void SetBeginningYear([NotNull] int beginningYear)
        {
            BeginningYear = Check.NotNull(beginningYear, nameof(beginningYear));
        }

        /// <summary>
        /// Correct skill knownledge level initialize.
        /// </summary>
        /// <param name="knownledgeLevel"></param>
        protected void SetKnownledgeLevel([NotNull] int knownledgeLevel)
        {
            KnownledgeLevel = Check.NotNull(knownledgeLevel, nameof(knownledgeLevel));
        }

        /// <summary>
        /// Correct skill group changing.
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        internal UserProfSkill ChangeGroup([NotNull] Guid groupId)
        {
            SetGroup(groupId);
            return this;
        }

        /// <summary>
        /// Correct skill beginning year changing.
        /// </summary>
        /// <param name="beginningYear"></param>
        /// <returns></returns>
        internal UserProfSkill ChangeBeginningYear([NotNull] int beginningYear)
        {
            SetBeginningYear(beginningYear);
            return this;
        }

        /// <summary>
        /// Correct skill knownledge level changing.
        /// </summary>
        /// <param name="knownledgeLevel"></param>
        /// <returns></returns>
        internal UserProfSkill ChangeKnownledgeLevel([NotNull] int knownledgeLevel)
        {
            SetKnownledgeLevel(knownledgeLevel);
            return this;
        }

        /// <summary>
        /// Correct skill end year changing.
        /// </summary>
        /// <param name="endYear"></param>
        /// <returns></returns>
        internal UserProfSkill ChangeEndYear([CanBeNull] int endYear)
        {
            EndYear = endYear;
            return this;
        }
    }
}
