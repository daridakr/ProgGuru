using Volo.Abp;

namespace Daridakr.ProgGuru.Entities.Groups.Exceptions
{
    public class GroupAlreadyExistsException : BusinessException
    {
        public GroupAlreadyExistsException(string title)
            : base(ProgGuruDomainErrorCodes.Groups.GroupAlreadyExists)
        {
            WithData("title", title);
        }
    }
}
