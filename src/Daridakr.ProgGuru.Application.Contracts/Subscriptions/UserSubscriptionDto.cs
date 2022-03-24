using System;
using Volo.Abp.Application.Dtos;

namespace Daridakr.ProgGuru.Subscriptions
{
    public class UserSubscriptionDto : CreationAuditedEntityDto<Guid>
    {
        /* CreatorId */

        public Guid UserId { get; set; }

        public string UserName { get; set; }

        public string UserUsername { get; set; }

        public string UserProfilePicture { get; set; }
    }
}
