using Daridakr.ProgGuru.Entities.Subscriptions.Exceptions;
using Daridakr.ProgGuru.Entities.Users.Exceptions;
using JetBrains.Annotations;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Identity;

namespace Daridakr.ProgGuru.Entities.Subscriptions
{
    public class UserSubscriptionManager : DomainService
    {
        private readonly IUserSubscriptionRepository _userSubscriptionRepository;
        private readonly IRepository<IdentityUser, Guid> _userRepository;
        
        public UserSubscriptionManager(
            IUserSubscriptionRepository userSubscriptionRepository,
            IRepository<IdentityUser, Guid> userRepository)
        {
            _userSubscriptionRepository = userSubscriptionRepository;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Creating new user to user subscription in a controlled way.
        /// </summary>
        /// <param name="creatorId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<UserSubscription> CreateAsync(
            [NotNull] Guid creatorId,
            [NotNull] Guid userId)
        {
            await checkIfUserExistsAsync(creatorId);
            await checkIfUserExistsAsync(userId);
            await disallowMultipleSubscriptionsAsync(creatorId, userId);

            return new UserSubscription(
               GuidGenerator.Create(),
               creatorId,
               userId
           );
        }

        /// <summary>
        /// Checks if a user exists.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="UserDoesntExistsException"></exception>
        protected async Task checkIfUserExistsAsync([NotNull] Guid userId)
        {
            Check.NotNull(userId, nameof(userId));
            var existingUser = await _userRepository.FindAsync(userId);
            if (existingUser == null) throw new UserDoesntExistsException(userId);
        }

        /// <summary>
        /// Throws an exception when user trying to subscribe to the same user.
        /// </summary>
        /// <param name="creatorId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="SubscriptionAlreadyExistsException"></exception>
        protected async Task disallowMultipleSubscriptionsAsync(
            [NotNull] Guid creatorId,
            [NotNull] Guid userId)
        {
            var existingUserSubscription = await checkIfSubscriptionNotExistsAsync(creatorId, userId);
            if (existingUserSubscription != null) throw new SubscriptionAlreadyExistsException();
        }

        public async Task<bool> CheckIfUserNotSubscribedAsync(
            [NotNull] Guid creatorId,
            [NotNull] Guid userId)
        {
            var existingUserSubscription = await checkIfSubscriptionNotExistsAsync(creatorId, userId);
            if (existingUserSubscription != null) return false;
            return true;
        }

        /// <summary>
        /// Checks if the user is subscribed to the user.
        /// </summary>
        /// <param name="creatorId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        private async Task<UserSubscription> checkIfSubscriptionNotExistsAsync(
            [NotNull] Guid creatorId,
            [NotNull] Guid userId)
        {
            Check.NotNull(creatorId, nameof(creatorId));
            Check.NotNull(creatorId, nameof(userId));
            var subscriptionsList = await _userSubscriptionRepository.GetListAsync();
            var userSubscriptions = subscriptionsList.Where(subscription => subscription.CreatorId == creatorId);
            return userSubscriptions.FirstOrDefault(userSubscription => userSubscription.UserId == userId);
        }
    }
}
