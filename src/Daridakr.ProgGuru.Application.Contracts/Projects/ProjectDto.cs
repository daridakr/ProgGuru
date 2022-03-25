using Daridakr.ProgGuru.Base;
using Daridakr.ProgGuru.Enums.Projects;
using System;

namespace Daridakr.ProgGuru.Projects
{
    [Serializable]
    public class ProjectDto : PostEntityDto
    {
        public Guid GroupId { get; set; }

        public string GroupTitle { get; set; }

        public double AverageRating { get; set; }

        public int StarsCount { get; set; }

        public DateTime PublishDate { get; set; }

        public DateTime RealeseDate { get; set; }

        public ProjectCategory Category { get; set; }

        public ProjectStatus Status { get; set; }

        public string GoToUseLink { get; set; }

        public string GoToGitLink { get; set; }
    }
}
