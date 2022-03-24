using Volo.Abp;

namespace Daridakr.ProgGuru.Entities.Projects.Exceptions
{
    public class CompletedProjectPublishDateLaterReleaseDateException : BusinessException
    {
        public CompletedProjectPublishDateLaterReleaseDateException()
          : base(ProgGuruDomainErrorCodes.Projects.CompletedProjectPublishDateLaterReleaseDate)
        {

        }
    }
}
