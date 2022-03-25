using Volo.Abp;

namespace Daridakr.ProgGuru.Entities.Projects.Exceptions
{
    public class AbandonedProjectRequiresPublishDateException : BusinessException
    {
        public AbandonedProjectRequiresPublishDateException()
           : base(ProgGuruDomainErrorCodes.Projects.AbandonedProjectRequiresPublishDate)
        {

        }
    }
}
