using System;
using Volo.Abp;

namespace Daridakr.ProgGuru.Entities.Groups.Exceptions
{
    public class GroupDoesntExistsException : BusinessException
    {
        public GroupDoesntExistsException(Guid id)
            : base(ProgGuruDomainErrorCodes.Groups.GroupDoesntExists)
        {
            WithData("id", id);
        }
    }
}
