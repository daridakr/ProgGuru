using System;
using Volo.Abp.Application.Dtos;

namespace Daridakr.ProgGuru.Projects
{
    public class TagLookupDto : EntityDto<Guid>
    {
        public string Name { get; set; }
    }
}
