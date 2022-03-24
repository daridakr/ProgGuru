using System;
using Volo.Abp.Application.Dtos;

namespace Daridakr.ProgGuru.Users.Specializations
{
    public class SpecializationDto : CreationAuditedEntityDto<Guid>
    {
        public string Name { get; set; }
    }
}
