using System;
using Volo.Abp.Application.Dtos;

namespace Daridakr.ProgGuru.Users.Courses
{
    public class UserCourseDto : CreationAuditedEntityDto<Guid>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string CoverImagePath { get; set; }

        public int ReceivingYear { get; set; }
    }
}
