using System;

namespace Daridakr.ProgGuru.Users.Skills
{
    public class ProfSkillDto : CommonSkillDto
    {
        /* CreatorId, Name */

        public Guid GroupId { get; set; }

        public string GroupCoverImagePath { get; set; }

        public int BeginningYear { get; set; }

        public int EndYear { get; set; }

        public int KnownledgeLevel { get; set; }
    }
}
