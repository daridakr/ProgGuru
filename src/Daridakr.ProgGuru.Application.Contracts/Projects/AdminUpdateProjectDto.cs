using Daridakr.ProgGuru.Base;
using Daridakr.ProgGuru.Enums.Projects;
using System;
using System.ComponentModel.DataAnnotations;

namespace Daridakr.ProgGuru.Projects
{
    public class AdminUpdateProjectDto : UpdatePostEntityDto
    {
        [Required]
        public Guid CreatorId { get; set; }

        [Required]
        public Guid GroupId { get; set; }

        [Required]
        public ProjectCategory Category { get; set; } = ProjectCategory.Other;

        [Required]
        public ProjectStatus Status { get; set; } = ProjectStatus.During;

        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime RealeseDate { get; set; }

        [DataType(DataType.Url)]
        public string GoToUseLink { get; set; }

        [DataType(DataType.Url)]
        public string GoToGitLink { get; set; }
    }
}
