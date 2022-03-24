using Volo.Abp;

namespace Daridakr.ProgGuru.Entities.Subscriptions.Exceptions
{
    public class SubscriptionAlreadyExistsException : BusinessException
    {
        public SubscriptionAlreadyExistsException()
          : base(ProgGuruDomainErrorCodes.Subscriptions.SubscriptionAlreadyExists)
        {

        }
    }
}
