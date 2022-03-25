using System;
using Volo.Abp.Application.Dtos;

namespace Daridakr.ProgGuru.Base
{
    public class InformingEntityDto : AuditedEntityDto<Guid>
    {
        /* CreatorId */

        public string CreatorName { get; set; }

        public string CreatorUserName { get; set; }

        public string CreatorProfilePicture { get; set; }

        public string Title { get; set; }

        public string Subtitle { get; set; }

        public string TextInformation { get; set; }
    }
}
