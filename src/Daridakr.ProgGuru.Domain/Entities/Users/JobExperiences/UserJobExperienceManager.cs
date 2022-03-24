using Daridakr.ProgGuru.Entities.Users.Exceptions;
using Daridakr.ProgGuru.Enums.Users;
using JetBrains.Annotations;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Daridakr.ProgGuru.Entities.Users.JobExperiences
{
    public class UserJobExperienceManager : DomainService
    {
        public UserJobExperienceManager()
        {

        }

        /// <summary>
        /// Creating new valid job experience in a controlled way.
        /// </summary>
        /// <param name="creatorId"></param>
        /// <param name="position"></param>
        /// <param name="company"></param>
        /// <param name="category"></param>
        /// <param name="location"></param>
        /// <param name="beginningDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public UserJobExperience CreateAsync(
           [NotNull] Guid creatorId,
           [NotNull] string position,
           [NotNull] string company,
           [CanBeNull] UserCategory category,
           [CanBeNull] string location,
           [NotNull] DateTime beginningDate,
           [CanBeNull] DateTime? endDate)
        {
            checkIfBeginningDateCorrect(beginningDate);
            checkIfEndDateCorrect(endDate, beginningDate);

            return new UserJobExperience(
                GuidGenerator.Create(),
                creatorId,
                position,
                company,
                category,
                location,
                beginningDate,
                endDate
            );
        }

        /// <summary>
        /// Checks if a beginning date correct.
        /// </summary>
        /// <param name="beginningDate"></param>
        /// <exception cref="BeginningDateOfJobInvalidException"></exception>
        private void checkIfBeginningDateCorrect([NotNull] DateTime beginningDate)
        {
            Check.NotNull(beginningDate, nameof(beginningDate));
            var nowDate = DateTime.Now;
            int minYear = nowDate.Year - 80;
            if (beginningDate.Year < minYear || beginningDate > nowDate) throw new BeginningDateOfJobInvalidException();
        }

        /// <summary>
        /// Checks if a end date correct.
        /// </summary>
        /// <param name="endDate"></param>
        /// <param name="beginningDate"></param>
        /// <exception cref="EndDateOfJobInvalidException"></exception>
        private void checkIfEndDateCorrect([CanBeNull] DateTime? endDate, [NotNull] DateTime beginningDate)
        {
            if (endDate != null)
            {
                Check.NotNull(beginningDate, nameof(beginningDate));
                var nowDate = DateTime.Now;
                if (endDate < beginningDate || endDate > nowDate) throw new EndDateOfJobInvalidException();
            }  
        }

        /// <summary>
        /// Updating valid job experience in a controlled way.
        /// </summary>
        /// <param name="jobExperience"></param>
        /// <param name="position"></param>
        /// <param name="company"></param>
        /// <param name="category"></param>
        /// <param name="location"></param>
        /// <param name="beginningDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public UserJobExperience UpdateAsync(
            [NotNull] UserJobExperience jobExperience,
            [NotNull] string position,
            [NotNull] string company,
            [CanBeNull] UserCategory category,
            [CanBeNull] string location,
            [NotNull] DateTime beginningDate,
            [CanBeNull] DateTime? endDate)
        {
            Check.NotNull(jobExperience, nameof(jobExperience));

            jobExperience.ChangePosition(position);
            jobExperience.ChangeCompany(company);
            jobExperience.ChangeCategory(category);
            jobExperience.ChangeLocation(location);
            ChangeBeginningDate(jobExperience, beginningDate);
            ChangeEndDate(jobExperience, endDate, beginningDate);

            return jobExperience;
        }

        /// <summary>
        /// Set new beginning date for user job experience.
        /// </summary>
        /// <param name="jobExperience"></param>
        /// <param name="newBeginningDate"></param>
        public void ChangeBeginningDate(
            [NotNull] UserJobExperience jobExperience,
            [NotNull] DateTime newBeginningDate)
        {
            Check.NotNull(jobExperience, nameof(jobExperience));
            checkIfBeginningDateCorrect(newBeginningDate);
            jobExperience.ChangeBeginningDate(newBeginningDate);
        }

        /// <summary>
        /// Set new end date for user job experience.
        /// </summary>
        /// <param name="jobExperience"></param>
        /// <param name="newEndDate"></param>
        /// <param name="beginningDate"></param>
        public void ChangeEndDate(
            [NotNull] UserJobExperience jobExperience,
            [CanBeNull] DateTime? newEndDate,
            [NotNull] DateTime beginningDate)
        {
            Check.NotNull(jobExperience, nameof(jobExperience));
            checkIfEndDateCorrect(newEndDate, beginningDate);
            jobExperience.ChangeEndDate(newEndDate);
        }
    }
}
