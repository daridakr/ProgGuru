using Volo.Abp;

namespace Daridakr.ProgGuru.Entities.Projects.Exceptions
{
    public class CompletedProjectInvalidPublishDateException : BusinessException
    {
        public CompletedProjectInvalidPublishDateException()
           : base(ProgGuruDomainErrorCodes.Projects.CompletedProjectInvalidPublishDate)
        {

        }
    }
}
