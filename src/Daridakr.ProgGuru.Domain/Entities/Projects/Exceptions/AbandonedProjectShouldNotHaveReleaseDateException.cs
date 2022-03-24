using Volo.Abp;

namespace Daridakr.ProgGuru.Entities.Projects.Exceptions
{
    public class AbandonedProjectShouldNotHaveReleaseDateException : BusinessException
    {
        public AbandonedProjectShouldNotHaveReleaseDateException()
           : base(ProgGuruDomainErrorCodes.Projects.AbandonedProjectShouldNotHaveReleaseDate)
        {

        }
    }
}
