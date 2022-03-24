using JetBrains.Annotations;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Daridakr.ProgGuru.Entities.Subscriptions
{
    /// <summary>
    /// User subscriptions to groups
    /// </summary>
    public class GroupSubscription : CreationAuditedAggregateRoot<Guid>
    {
        /* CreatorId */

        public Guid GroupId { get; protected set; }

        /// <summary>
        /// This constructor is used by the ORM/database provider.
        /// </summary>
        protected GroupSubscription() { }

        internal GroupSubscription(
            [NotNull] Guid id,
            [NotNull] Guid creatorId,
            [NotNull] Guid groupId)
            : base(id)
        {
            SetCreator(creatorId);
            SetGroup(groupId);
        }

        /// <summary>
        /// Correct creator initialize.
        /// </summary>
        /// <param name="creatorId"></param>
        protected void SetCreator([NotNull] Guid creatorId)
        {
            CreatorId = Check.NotNull(creatorId, nameof(creatorId));
        }

        /// <summary>
        /// Correct group initialize.
        /// </summary>
        /// <param name="groupId"></param>
        protected void SetGroup([NotNull] Guid groupId)
        {
            GroupId = Check.NotNull(groupId, nameof(groupId));
        }
    }
}
