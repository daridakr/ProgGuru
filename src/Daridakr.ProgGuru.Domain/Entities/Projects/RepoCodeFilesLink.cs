using System;
using Volo.Abp.Domain.Entities;

namespace Daridakr.ProgGuru.Entities.Projects
{
    public class RepoCodeFilesLink : Entity<Guid>
    {
        public Guid ProjectId { get; private set; }

        public string Link { get; private set; }
    }
}
