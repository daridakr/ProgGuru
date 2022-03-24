using Volo.Abp;

namespace Daridakr.ProgGuru.Entities.Groups.Exceptions
{
    public class GroupIssueYearInvalidException : BusinessException
    {
        public GroupIssueYearInvalidException()
           : base(ProgGuruDomainErrorCodes.Groups.GroupIssueYearInvalid)
        {

        }
    }
}
