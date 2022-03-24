using Daridakr.ProgGuru.Base;

namespace Daridakr.ProgGuru.Groups
{
    public class GroupDto : InformingEntityDto
    {
        /* Title, subtitle, textInformation, CreatorName, CreatorUserName, CreatorProfilePicture, CreatorId */

        public string Developer { get; set; }

        public string Website { get; set; }

        public int IssueYear { get; set; }

        public string CoverImagePath { get; set; }

        // влияние list<group>
        // подписчики
        // статьи
        // комментарии
    }
}
