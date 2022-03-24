using Daridakr.ProgGuru.Base;
using Daridakr.ProgGuru.Consts;
using System.ComponentModel.DataAnnotations;

namespace Daridakr.ProgGuru.Groups
{
    public class CreateGroupDto : CreateInformingEntityDto
    {
        [Required]
        [DataType(DataType.Text)]
        [StringLength(GroupConsts.MaxTitleLength)]
        public new string Title { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(GroupConsts.MaxSubtitleLength)]
        public new string Subtitle { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(GroupConsts.MaxTextInformationLength)]
        public new string TextInformation { get; set; }

        [Required]
        [StringLength(GroupConsts.MaxDeveloperLength)]
        public string Developer { get; set; }

        [StringLength(GroupConsts.MaxWebsiteLength)]
        public string Website { get; set; }

        [Required]
        //[StringLength(GroupConsts.MaxIssueYearLength)]
        public int IssueYear { get; set; }

        [Required]
        [DataType(DataType.ImageUrl)]
        public string CoverImagePath { get; set; }
    }
}
