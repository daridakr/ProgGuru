using JetBrains.Annotations;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Daridakr.ProgGuru.Entities.Subscriptions
{
    /// <summary>
    /// User subscriptions to users
    /// </summary>
    public class UserSubscription : CreationAuditedAggregateRoot<Guid>
    {
        /* CreatorId */

        public Guid UserId { get; protected set; }

        // <summary>
        /// This constructor is used by the ORM/database provider.
        /// </summary>
        protected UserSubscription() { }

        internal UserSubscription(
            [NotNull] Guid id,
            [NotNull] Guid creatorId,
            [NotNull] Guid userId)
            : base(id)
        {
            SetCreator(creatorId);
            SetUser(userId);
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
        /// Correct user initialize.
        /// </summary>
        /// <param name="userId"></param>
        protected void SetUser([NotNull] Guid userId)
        {
            UserId = Check.NotNull(userId, nameof(userId));
        }
    }
}
