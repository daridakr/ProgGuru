using Volo.Abp;

namespace Daridakr.ProgGuru.Entities.Users.Exceptions
{
    public class EndDateOfJobInvalidException : BusinessException
    {
        public EndDateOfJobInvalidException()
           : base(ProgGuruDomainErrorCodes.Users.EndDateOfJobInvalid)
        {

        }
    }
}
