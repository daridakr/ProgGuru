using Daridakr.ProgGuru.Consts;
using System;
using System.ComponentModel.DataAnnotations;

namespace Daridakr.ProgGuru.Users.Courses
{
    public class CreateUserCourseDto
    {
        [Required]
        public Guid CreatorId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(UserConsts.MaxCourseLength)]
        public string Name { get; set; }

        [DataType(DataType.Text)]
        [StringLength(UserConsts.MaxCourseDescriptionLength)]
        public string Description { get; set; }

        [DataType(DataType.ImageUrl)]
        public string CoverImagePath { get; set; }

        [Required]
        public int ReceivingYear { get; set; }
    }
}
