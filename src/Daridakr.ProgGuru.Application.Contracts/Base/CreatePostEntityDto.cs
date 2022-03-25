using Daridakr.ProgGuru.Consts;
using System.ComponentModel.DataAnnotations;

namespace Daridakr.ProgGuru.Base
{
    public class CreatePostEntityDto : CreateInformingEntityDto
    {
        [Required]
        [DataType(DataType.Text)]
        [StringLength(PostConsts.MaxTitleLength)]
        public new string Title { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(PostConsts.MaxSubtitleLength)]
        public new string Subtitle { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(PostConsts.MaxTextInformationLength)]
        public new string TextInformation { get; set; }

        [Required]
        [DataType(DataType.ImageUrl)]
        public string CoverImagePath { get; set; }
    }
}
