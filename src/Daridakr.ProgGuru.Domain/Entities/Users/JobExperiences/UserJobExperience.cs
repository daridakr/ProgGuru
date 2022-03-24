using Daridakr.ProgGuru.Consts;
using Daridakr.ProgGuru.Enums.Users;
using JetBrains.Annotations;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Daridakr.ProgGuru.Entities.Users.JobExperiences
{
    public class UserJobExperience : FullAuditedAggregateRoot<Guid>
    {
        // CreatorId

        public string Position { get; private set; }

        public string CompanyName { get; private set; }

        public UserCategory PositionCategory { get; private set; }

        public string Location { get; private set; }

        public DateTime BeginningDate { get; private set; }

        public DateTime? EndDate { get; private set; }

        public bool IsCurrent { get; private set; }

        /// <summary>
        /// This constructor is used by the ORM/database provider.
        /// </summary>
        protected UserJobExperience() : base() { }

        /// <summary>
        /// This constructor is used for creating new user job experience.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="creatorId"></param>
        /// <param name="position"></param>
        /// <param name="company"></param>
        /// <param name="category"></param>
        /// <param name="location"></param>
        /// <param name="beginningDate"></param>
        /// <param name="endDate"></param>
        internal UserJobExperience(
            [NotNull] Guid id,
            [NotNull] Guid creatorId,
            [NotNull] string position,
            [NotNull] string company,
            [CanBeNull] UserCategory category,
            [CanBeNull] string location,
            [NotNull] DateTime beginningDate,
            [CanBeNull] DateTime? endDate)
            : base(id)
        {
            SetCreator(creatorId);
            SetPosition(position);
            SetCompany(company);
            PositionCategory = category;
            SetLocation(location);
            SetBeginningDate(beginningDate);
            SetEndDate(endDate);
        }

        /// <summary>
        /// Correct creator of job experience initialize.
        /// </summary>
        /// <param name="creatorId"></param>
        protected virtual void SetCreator([NotNull] Guid creatorId)
        {
            CreatorId = Check.NotNull(creatorId, nameof(creatorId));
        }

        /// <summary>
        /// Correct position of job experience initialize.
        /// </summary>
        /// <param name="position"></param>
        protected virtual void SetPosition([NotNull] string position)
        {
            Position = Check.NotNullOrWhiteSpace(
                position,
                nameof(position),
                maxLength: UserConsts.MaxPositionLength,
                minLength: UserConsts.MinPositionLength
            );
        }

        /// <summary>
        /// Correct company name of job experience initialize.
        /// </summary>
        /// <param name="company"></param>
        protected virtual void SetCompany([NotNull] string company)
        {
            CompanyName = Check.NotNullOrWhiteSpace(
                company,
                nameof(company),
                maxLength: UserConsts.MaxCompanyNameLength,
                minLength: UserConsts.MinCompanyNameLength
            );
        }

        /// <summary>
        /// Correct work location of job experience initialize.
        /// </summary>
        /// <param name="location"></param>
        protected virtual void SetLocation([CanBeNull] string location)
        {
            Location = location == null ? location : Check.NotNullOrWhiteSpace(
                location,
                nameof(location),
                maxLength: UserConsts.MaxLocationLength,
                minLength: UserConsts.MinLocationLength
            );
        }

        /// <summary>
        /// Correct beginning date of job experience initialize.
        /// </summary>
        /// <param name="beginningDate"></param>
        protected virtual void SetBeginningDate([NotNull] DateTime beginningDate)
        {
            BeginningDate = Check.NotNull(beginningDate, nameof(beginningDate));
        }

        /// <summary>
        /// Correct end date of job experience initialize.
        /// </summary>
        /// <param name="endDate"></param>
        protected virtual void SetEndDate([CanBeNull] DateTime? endDate)
        {
            EndDate = endDate;
            IsCurrent = endDate == null ? true : false; 
        }

        /// <summary>
        /// Correct position of user job experience changing.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        internal UserJobExperience ChangePosition([NotNull] string position)
        {
            SetPosition(position);
            return this;
        }

        /// <summary>
        /// Correct company of user job experience changing.
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        internal UserJobExperience ChangeCompany([NotNull] string company)
        {
            SetCompany(company);
            return this;
        }

        /// <summary>
        /// Correct position category of user job experience changing.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        internal UserJobExperience ChangeCategory([CanBeNull] UserCategory category)
        {
            PositionCategory = category;
            return this;
        }

        /// <summary>
        /// Correct location of user job experience changing.
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        internal UserJobExperience ChangeLocation([CanBeNull] string location)
        {
            SetLocation(location);
            return this;
        }

        /// <summary>
        /// Correct beginning date of user job experience changing.
        /// </summary>
        /// <param name="beginningDate"></param>
        /// <returns></returns>
        internal UserJobExperience ChangeBeginningDate([NotNull] DateTime beginningDate)
        {
            SetBeginningDate(beginningDate);
            return this;
        }

        /// <summary>
        /// Correct end date of user job experience changing.
        /// </summary>
        /// <param name="endDate"></param>
        /// <returns></returns>
        internal UserJobExperience ChangeEndDate([CanBeNull] DateTime? endDate)
        {
            SetEndDate(endDate);
            return this;
        }
    }
}
