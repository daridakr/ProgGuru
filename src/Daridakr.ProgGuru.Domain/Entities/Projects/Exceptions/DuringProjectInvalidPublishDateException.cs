using Volo.Abp;

namespace Daridakr.ProgGuru.Entities.Projects.Exceptions
{
    public class DuringProjectInvalidPublishDateException : BusinessException
    {
        public DuringProjectInvalidPublishDateException()
          : base(ProgGuruDomainErrorCodes.Projects.DuringProjectInvalidPublishDate)
        {

        }
    }
}
