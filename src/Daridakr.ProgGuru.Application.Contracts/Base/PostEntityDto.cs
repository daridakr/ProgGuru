namespace Daridakr.ProgGuru.Base
{
    public class PostEntityDto : InformingEntityDto
    {
        /* Title, subtitle, textInformation, CreatorId */

        public int Views { get; set; }

        public int SavesAmount { get; private set; }

        public string CoverImagePath { get; set; }

        //public List<TagDto> Tags { get; set; }

        public int CommentCount { get; set; }
    }
}
