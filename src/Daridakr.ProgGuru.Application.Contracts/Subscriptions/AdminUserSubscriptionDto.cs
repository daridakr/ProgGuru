using System;
using Volo.Abp.Application.Dtos;

namespace Daridakr.ProgGuru.Subscriptions
{
    public class AdminUserSubscriptionDto : CreationAuditedEntityDto<Guid>
    {
        /* CreatorId */

        public Guid UserId { get; set; }

        public string CreatorUsername { get; set; }

        public string UserUsername { get; set; }
    }
}
