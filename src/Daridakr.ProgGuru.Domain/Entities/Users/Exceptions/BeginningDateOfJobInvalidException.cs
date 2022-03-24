using Volo.Abp;

namespace Daridakr.ProgGuru.Entities.Users.Exceptions
{
    public class BeginningDateOfJobInvalidException : BusinessException
    {
        public BeginningDateOfJobInvalidException()
           : base(ProgGuruDomainErrorCodes.Users.BeginningDateOfJob)
        {

        }
    }
}
