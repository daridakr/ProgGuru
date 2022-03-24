using System;
using Volo.Abp;

namespace Daridakr.ProgGuru.Entities.Users.Exceptions
{
    public class UserDoesntExistsException : BusinessException
    {
        public UserDoesntExistsException(Guid id)
           : base(ProgGuruDomainErrorCodes.Users.UserDoesntExists)
        {
            WithData("id", id);
        }
    }
}
