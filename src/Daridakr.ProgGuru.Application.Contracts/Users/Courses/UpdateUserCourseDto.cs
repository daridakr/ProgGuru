using Daridakr.ProgGuru.Consts;
using System.ComponentModel.DataAnnotations;

namespace Daridakr.ProgGuru.Users.Courses
{
    public class UpdateUserCourseDto
    {
        [Required]
        [DataType(DataType.Text)]
        [StringLength(UserConsts.MaxCourseLength)]
        public string Name { get; set; }

        [DataType(DataType.Text)]
        [StringLength(UserConsts.MaxCourseDescriptionLength)]
        public string Description { get; set; }

        [DataType(DataType.ImageUrl)]
        public string CoverImagePath { get; set; }
    }
}
