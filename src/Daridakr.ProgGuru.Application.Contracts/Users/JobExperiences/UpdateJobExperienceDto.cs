using Daridakr.ProgGuru.Consts;
using Daridakr.ProgGuru.Enums.Users;
using System;
using System.ComponentModel.DataAnnotations;

namespace Daridakr.ProgGuru.Users.JobExperiences
{
    public class UpdateJobExperienceDto
    {
        [Required]
        [DataType(DataType.Text)]
        [StringLength(UserConsts.MaxPositionLength)]
        public string Position { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(UserConsts.MaxCompanyNameLength)]
        public string CompanyName { get; set; }

        public UserCategory PositionCategory { get; set; }

        [DataType(DataType.Text)]
        [StringLength(UserConsts.MaxLocationLength)]
        public string Location { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime BeginningDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }
    }
}
