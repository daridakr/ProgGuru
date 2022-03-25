using Daridakr.ProgGuru.Enums.Users;
using System.ComponentModel.DataAnnotations;

namespace Daridakr.ProgGuru.Users.Skills
{
    public class CreateLanguageSkillDto : CreateCommonSkillDto
    {
        /* CreatorId, Name */

        [Required]
        public UserLanguageLevel LanguageLevel { get; set; } = UserLanguageLevel.Beginner;
    }
}
