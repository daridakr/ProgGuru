using Volo.Abp;

namespace Daridakr.ProgGuru.Entities.Projects.Exceptions
{
    public class DuringProjectRequiresRealeseDateException : BusinessException
    {
        public DuringProjectRequiresRealeseDateException()
           : base(ProgGuruDomainErrorCodes.Projects.DuringProjectRequiresRealeseDate)
        {

        }
    }
}
