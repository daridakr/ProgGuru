using System;
using Volo.Abp.Application.Dtos;

namespace Daridakr.ProgGuru.Subscriptions
{
    public class AdminGroupSubscriptionDto : CreationAuditedEntityDto<Guid>
    {
        /* CreatorId */

        public Guid GroupId { get; set; }

        public string GroupTitle { get; set; }

        public string CreatorUserName { get; set; }
    }
}
