using System;
using System.ComponentModel.DataAnnotations;

namespace Daridakr.ProgGuru.Subscriptions
{
    public class CreateUserSubscriptionDto
    {

        [Required]
        public Guid CreatorId { get; set; }

        [Required]
        public Guid UserId { get; set; }
    }
}
