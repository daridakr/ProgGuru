using Volo.Abp;

namespace Daridakr.ProgGuru.Entities.Users.Exceptions
{
    public class KnownledgeLevelInvalidException : BusinessException
    {
        public KnownledgeLevelInvalidException()
           : base(ProgGuruDomainErrorCodes.Users.KnownledgeLevelInvalid)
        {

        }
    }
}
