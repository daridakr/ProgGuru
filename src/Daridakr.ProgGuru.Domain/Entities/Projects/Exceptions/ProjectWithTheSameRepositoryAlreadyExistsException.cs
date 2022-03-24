using Volo.Abp;

namespace Daridakr.ProgGuru.Entities.Projects.Exceptions
{
    public class ProjectWithTheSameRepositoryAlreadyExistsException : BusinessException
    {
        public ProjectWithTheSameRepositoryAlreadyExistsException()
           : base(ProgGuruDomainErrorCodes.Projects.ProjectWithTheSameRepositoryAlreadyExists)
        {

        }
    }
}
