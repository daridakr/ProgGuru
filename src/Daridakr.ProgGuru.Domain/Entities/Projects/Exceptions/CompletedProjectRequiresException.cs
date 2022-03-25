using Volo.Abp;

namespace Daridakr.ProgGuru.Entities.Projects.Exceptions
{
    public class CompletedProjectRequiresException : BusinessException
    {
        public CompletedProjectRequiresException(string date)
            : base(ProgGuruDomainErrorCodes.Projects.CompletedProjectRequires)
        {
            WithData("date", date);
        }
    }
}
