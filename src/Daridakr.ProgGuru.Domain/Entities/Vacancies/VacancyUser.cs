using JetBrains.Annotations;
using System;
using Volo.Abp.Domain.Entities;

namespace Daridakr.ProgGuru.Entities.Vacancies
{
    public class VacancyUser : Entity
    {
        public Guid VacancyId { get; private set; }

        public Guid UserId { get; private set; }

        private VacancyUser() { }

        internal VacancyUser(
            [NotNull] Guid vacancyId,
            [NotNull] Guid userId)
        {
            VacancyId = vacancyId;
            UserId = userId;
        }

        public override object[] GetKeys()
        {
            return new object[] { VacancyId, UserId };
        }
    }
}
