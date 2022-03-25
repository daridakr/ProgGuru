using System;
using Volo.Abp.Application.Dtos;

namespace Daridakr.ProgGuru.Projects
{
    public class UserLookupDto : EntityDto<Guid>
    {
        public string UserName { get; set; }
    }
}
