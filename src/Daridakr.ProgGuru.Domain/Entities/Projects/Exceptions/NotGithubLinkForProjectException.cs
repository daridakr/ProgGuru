using Volo.Abp;

namespace Daridakr.ProgGuru.Entities.Projects.Exceptions
{
    public class NotGithubLinkForProjectException : BusinessException
    {
        public NotGithubLinkForProjectException()
           : base(ProgGuruDomainErrorCodes.Projects.NotGithubLinkForProjectE)
        {

        }
    }
}
