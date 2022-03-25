using Volo.Abp;

namespace Daridakr.ProgGuru.Entities.Groups.Exceptions
{
    public class GroupWebsiteInvalidException : BusinessException
    {
        public GroupWebsiteInvalidException(string website)
            : base(ProgGuruDomainErrorCodes.Groups.GroupWebsiteInvalid)
        {
            WithData("website", website);
        }
    }
}
