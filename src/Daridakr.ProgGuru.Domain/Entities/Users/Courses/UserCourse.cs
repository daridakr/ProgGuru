using Daridakr.ProgGuru.Consts;
using JetBrains.Annotations;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Daridakr.ProgGuru.Entities.Users.Courses
{
    public class UserCourse : CreationAuditedAggregateRoot<Guid>
    {
        // Creator 

        public string Name { get; private set; }

        public string Description { get; private set; }

        public string CoverImagePath { get; private set; }

        public int ReceivingYear { get; private set; }

        /// <summary>
        /// This constructor is used by the ORM/database provider.
        /// </summary>
        protected UserCourse() : base() { }

        /// <summary>
        /// This constructor is used for creating new user course.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="creatorId"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="coverImagePath"></param>
        /// <param name="receivingYear"></param>
        internal UserCourse(
            [NotNull] Guid id,
            [NotNull] Guid creatorId,
            [NotNull] string name,
            [CanBeNull] string description,
            [CanBeNull] string coverImagePath,
            [NotNull] int receivingYear)
            : base(id)
        {
            SetCreator(creatorId);
            SetName(name);
            SetDescription(description);
            CoverImagePath = coverImagePath;
            SetReceivingYear(receivingYear);
        }

        /// <summary>
        /// Correct course creator initialize.
        /// </summary>
        /// <param name="creatorId"></param>
        protected virtual void SetCreator([NotNull] Guid creatorId)
        {
            CreatorId = Check.NotNull(creatorId, nameof(creatorId));
        }

        /// <summary>
        /// Correct course name initialize.
        /// </summary>
        /// <param name="name"></param>
        protected virtual void SetName([NotNull] string name)
        {
            Name = Check.NotNullOrWhiteSpace(
                name,
                nameof(name),
                maxLength: UserConsts.MaxCourseLength,
                minLength: UserConsts.MinCourseLength
            );
        }

        /// <summary>
        /// Correct course description initialize.
        /// </summary>
        /// <param name="name"></param>
        protected virtual void SetDescription([CanBeNull] string description)
        {
            Description = description == null ? description : Check.NotNullOrWhiteSpace(
                description,
                nameof(description),
                maxLength: UserConsts.MaxCourseDescriptionLength,
                minLength: UserConsts.MinCourseDescriptionLength
            );
        }

        /// <summary>
        /// Correct course receiving year initialize.
        /// </summary>
        /// <param name="receivingYear"></param>
        protected virtual void SetReceivingYear([NotNull] int receivingYear)
        {
            ReceivingYear = receivingYear <= 0 ? 1 : receivingYear; 
        }

        /// <summary>
        /// Correct course name changing.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        internal UserCourse ChangeName([NotNull] string name)
        {
            SetName(name);
            return this;
        }

        /// <summary>
        /// Correct course description changing.
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        internal UserCourse ChangeDescription([CanBeNull] string description)
        {
            SetDescription(description);
            return this;
        }

        /// <summary>
        /// Correct course cover changing.
        /// </summary>
        /// <param name="coverImagePath"></param>
        /// <returns></returns>
        internal UserCourse ChangeCover([CanBeNull] string coverImagePath)
        {
            CoverImagePath = coverImagePath;
            return this;
        }
    }
}
