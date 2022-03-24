using Volo.Abp;

namespace Daridakr.ProgGuru.Entities.Projects.Exceptions
{
    public class AbandonedProjectInvalidPublishDateException : BusinessException
    {
        public AbandonedProjectInvalidPublishDateException()
           : base(ProgGuruDomainErrorCodes.Projects.AbandonedProjectInvalidPublishDate)
        {

        }
    }
}
