using Volo.Abp;

namespace Daridakr.ProgGuru.Entities.Users.Exceptions
{
public class SkillAlreadyExistsException : BusinessException
{
    public SkillAlreadyExistsException(string name)
        : base(ProgGuruDomainErrorCodes.Users.SkillAlreadyExists)
    {
        WithData("name", name);
    }
}
}
