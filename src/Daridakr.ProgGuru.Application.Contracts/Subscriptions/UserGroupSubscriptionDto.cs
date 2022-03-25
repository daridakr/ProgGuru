using System;
using Volo.Abp.Application.Dtos;

namespace Daridakr.ProgGuru.Subscriptions
{
    public class UserGroupSubscriptionDto : CreationAuditedEntityDto<Guid>
    {
        /* CreatorId */

        public Guid GroupId { get; set; }

        public string GroupTitle { get; set; }

        public string GroupSubtitle { get; set; }

        public string GroupCoverImagePath { get; set; }
    }
}
