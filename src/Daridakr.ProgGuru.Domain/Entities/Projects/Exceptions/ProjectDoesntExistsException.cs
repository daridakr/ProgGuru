using System;
using Volo.Abp;

namespace Daridakr.ProgGuru.Entities.Projects.Exceptions
{
    public class ProjectDoesntExistsException : BusinessException
    {
        public ProjectDoesntExistsException(Guid id)
            : base(ProgGuruDomainErrorCodes.Projects.ProjectDoesntExists)
        {
            WithData("id", id);
        }
    }
}
