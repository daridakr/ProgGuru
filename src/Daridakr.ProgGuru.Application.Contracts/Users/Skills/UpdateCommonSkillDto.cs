using Daridakr.ProgGuru.Consts;
using System.ComponentModel.DataAnnotations;

namespace Daridakr.ProgGuru.Users.Skills
{
    public class UpdateCommonSkillDto
    {
        [Required]
        [DataType(DataType.Text)]
        [StringLength(UserConsts.MaxSkillNameLength)]
        public string Name { get; set; }
    }
}
