using Volo.Abp;

namespace Daridakr.ProgGuru.Entities.Projects.Exceptions
{
    public class DuringProjectInvalidRealeseDateException : BusinessException
    {
        public DuringProjectInvalidRealeseDateException()
          : base(ProgGuruDomainErrorCodes.Projects.DuringProjectInvalidRealeseDate)
        {

        }
    }
}
