using Volo.Abp;

namespace Daridakr.ProgGuru.Entities.Users.Exceptions
{
    public class ReceivingYearCourseInvalidException : BusinessException
    {
        public ReceivingYearCourseInvalidException()
           : base(ProgGuruDomainErrorCodes.Users.ReceivingYearCourseInvalid)
        {

        }
    }
}
