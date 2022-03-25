using Volo.Abp;

namespace Daridakr.ProgGuru.Entities.Projects.Exceptions
{
    public class ProjectAlreadyExistsException : BusinessException
    {
        public ProjectAlreadyExistsException()
          : base(ProgGuruDomainErrorCodes.Projects.ProjectAlreadyExists)
        {

        }
    }
}
