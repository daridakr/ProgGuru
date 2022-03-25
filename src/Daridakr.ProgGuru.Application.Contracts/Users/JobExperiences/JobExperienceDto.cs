using Daridakr.ProgGuru.Enums.Users;
using System;
using Volo.Abp.Application.Dtos;

namespace Daridakr.ProgGuru.Users.JobExperiences
{
    public class JobExperienceDto : FullAuditedEntityDto<Guid>
    {
        public string Position { get; set; }

        public string CompanyName { get; set; }

        public UserCategory PositionCategory { get; set; }

        public string Location { get; set; }

        public DateTime BeginningDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsCurrent { get; set; }
    }
}
