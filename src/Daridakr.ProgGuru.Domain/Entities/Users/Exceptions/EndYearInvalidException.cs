using Volo.Abp;

namespace Daridakr.ProgGuru.Entities.Users.Exceptions
{
    public class EndYearInvalidException : BusinessException
    {
        public EndYearInvalidException()
           : base(ProgGuruDomainErrorCodes.Users.EndYearInvalid)
        {

        }
    }
}
