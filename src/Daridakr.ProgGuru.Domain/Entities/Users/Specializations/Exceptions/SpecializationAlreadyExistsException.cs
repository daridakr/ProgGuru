using Volo.Abp;

namespace Daridakr.ProgGuru.Entities.Users.Specializations.Exceptions
{
    public class SpecializationAlreadyExistsException : BusinessException
    {
        public SpecializationAlreadyExistsException(string name)
            : base(ProgGuruDomainErrorCodes.Specializations.SpecializationAlreadyExists)
        {
            WithData("name", name);
        }
    }
}
