using Daridakr.ProgGuru.Entities.Users.Exceptions;
using JetBrains.Annotations;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Daridakr.ProgGuru.Entities.Users.Courses
{
    public class UserCourseManager : DomainService
    {
        public UserCourseManager()
        {

        }

        /// <summary>
        /// Creating new valid course in a controlled way.
        /// </summary>
        /// <param name="creatorId"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="coverImagePath"></param>
        /// <param name="receivingYear"></param>
        /// <returns></returns>
        public UserCourse CreateAsync(
           [NotNull] Guid creatorId,
           [NotNull] string name,
           [CanBeNull] string description,
           [CanBeNull] string coverImagePath,
           [NotNull] int receivingYear)
        {
            checkIfReceivingYearCorrect(receivingYear);

            return new UserCourse(
                GuidGenerator.Create(),
                creatorId,
                name,
                description,
                coverImagePath,
                receivingYear
            );
        }

        /// <summary>
        /// Checks if a receiving year correct.
        /// </summary>
        /// <param name="receivingYear"></param>
        /// <exception cref="ReceivingYearCourseInvalidException"></exception>
        private void checkIfReceivingYearCorrect([NotNull] int receivingYear)
        {
            Check.NotNull(receivingYear, nameof(receivingYear));
            var nowYear = DateTime.Now.Year;
            int minYear = nowYear - 80;
            if (receivingYear < minYear || receivingYear > nowYear) throw new ReceivingYearCourseInvalidException();
        }

        /// <summary>
        /// Updating valid user course in a controlled way.
        /// </summary>
        /// <param name="userCourse"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="coverImagePath"></param>
        /// <returns></returns>
        public UserCourse UpdateAsync(
            [NotNull] UserCourse userCourse,
            [NotNull] string name,
            [CanBeNull] string description,
            [CanBeNull] string coverImagePath)
        {
            Check.NotNull(userCourse, nameof(userCourse));

            userCourse.ChangeName(name);
            userCourse.ChangeDescription(description);
            userCourse.ChangeCover(coverImagePath);

            return userCourse;
        }
    }
}
