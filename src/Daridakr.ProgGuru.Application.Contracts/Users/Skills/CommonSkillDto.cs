using System;
using Volo.Abp.Application.Dtos;

namespace Daridakr.ProgGuru.Users.Skills
{
    public class CommonSkillDto : AuditedEntityDto<Guid>
    {
        /* CreatorId */

        public string Name { get; set; }
    }
}
