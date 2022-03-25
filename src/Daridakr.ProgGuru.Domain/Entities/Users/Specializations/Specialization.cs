using Daridakr.ProgGuru.Consts;
using JetBrains.Annotations;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Daridakr.ProgGuru.Entities.Users.Specializations
{
    public class Specialization : CreationAuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }

        protected Specialization() : base() { }

        internal Specialization([NotNull] Guid? creatorId, [NotNull] string name)
        {
            CreatorId = creatorId;
            SetName(name);
        }

        private void SetName([NotNull] string name)
        {
            Name = Check.NotNullOrWhiteSpace(
                name,
                nameof(name),
                maxLength: UserConsts.MaxSpecializationLength,
                minLength: UserConsts.MinSpecializationLength
            );
        }
    }
}
