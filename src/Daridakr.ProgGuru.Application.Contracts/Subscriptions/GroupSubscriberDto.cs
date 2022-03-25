using System;
using Volo.Abp.Application.Dtos;

namespace Daridakr.ProgGuru.Subscriptions
{
    public class GroupSubscriberDto : CreationAuditedEntityDto<Guid>
    {
        /* CreatorId */

        public Guid GroupId { get; set; }

        public string CreatorName { get; set; }

        public string CreatorUserName { get; set; }

        public string CreatorProfilePicture { get; set; }
    }
}
