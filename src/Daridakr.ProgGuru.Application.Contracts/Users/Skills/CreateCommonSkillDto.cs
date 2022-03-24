using Daridakr.ProgGuru.Consts;
using System;
using System.ComponentModel.DataAnnotations;

namespace Daridakr.ProgGuru.Users.Skills
{
    public class CreateCommonSkillDto
    {
        [Required]
        public Guid CreatorId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(UserConsts.MaxSkillNameLength)]
        public string Name { get; set; }
    }
}
