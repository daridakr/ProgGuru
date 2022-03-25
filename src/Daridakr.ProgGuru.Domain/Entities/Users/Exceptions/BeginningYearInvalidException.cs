using Volo.Abp;

namespace Daridakr.ProgGuru.Entities.Users.Exceptions
{
    public class BeginningYearInvalidException : BusinessException
    {
        public BeginningYearInvalidException(int startYear, int endYear)
           : base(ProgGuruDomainErrorCodes.Users.BeginningYearInvalid)
        {
            WithData("0", startYear);
            WithData("1", endYear);
        }
    }
}
