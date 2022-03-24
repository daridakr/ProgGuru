using System;
using System.ComponentModel.DataAnnotations;

namespace Daridakr.ProgGuru.Users.Skills
{
    public class UpdateProfSkillDto
    {
        [Required]
        public Guid GroupId { get; set; }

        [Required]
        public int BeginningYear { get; set; }

        public int EndYear { get; set; }

        [Required]
        public int KnownledgeLevel { get; set; }
    }
}
