using System;
using System.ComponentModel.DataAnnotations;

namespace Daridakr.ProgGuru.Subscriptions
{
    public class CreateGroupSubscriptionDto
    {
        [Required]
        public Guid CreatorId { get; set; }

        [Required]
        public Guid GroupId { get; set; }
    }
}
