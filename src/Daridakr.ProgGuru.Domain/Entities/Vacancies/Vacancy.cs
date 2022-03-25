using Daridakr.ProgGuru.Entities.Base;
using Daridakr.ProgGuru.Enums.Users;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;

namespace Daridakr.ProgGuru.Entities.Vacancies
{
    /// <summary>
    /// Published company vacancies.
    /// </summary>
    public class Vacancy : InformingEntity
    {
        /* Title, Subtitle, TextInformation */

        public Guid CompanyId { get; private set; }

        public Guid GroupId { get; private set; }

        //public ICollection<Tag_Vacancy> Tags { get; private set; }

        public UserCategory RequiredUserCategory { get; private set; }

        public int MinNumberOfJobs { get; private set; }

        public int RequiredJobExperience { get; private set; }

        public int RequiredNumOfProjects { get; private set; }

        //требуемые навыки
        //public ICollection<UserSkill> RequiredSkills { get; private set; }

        //требуемые языки

        public string Location { get; private set; }

        public int Salary { get; private set; }

        public int MaxSalary { get; private set; }

        public ICollection<VacancyUser> WillingUsers { get; private set; }

        protected override void SetSubtitle([NotNull] string subtitle)
        {
            throw new NotImplementedException();
        }

        protected override void SetTextInformation([NotNull] string textInformation)
        {
            throw new NotImplementedException();
        }

        protected override void SetTitle([NotNull] string title)
        {
            throw new NotImplementedException();
        }

        //(удалённая работа)?
    }
}
