using System;
using Volo.Abp.Application.Dtos;

namespace Daridakr.ProgGuru.Projects
{
    public class GroupLookupDto : EntityDto<Guid>
    {
        public string Title { get; set; }
    }
}
