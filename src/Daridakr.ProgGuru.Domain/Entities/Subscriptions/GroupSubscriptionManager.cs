using Daridakr.ProgGuru.Entities.Groups;
using Daridakr.ProgGuru.Entities.Groups.Exceptions;
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
    public class GroupSubscriptionManager : DomainService
    {
        private readonly IGroupSubscriptionRepository _groupSubscriptionRepository;
        private readonly IRepository<IdentityUser, Guid> _userRepository;
        private readonly IGroupRepository _groupRepository;

        public GroupSubscriptionManager(
            IGroupSubscriptionRepository groupSubscriptionRepository,
            IRepository<IdentityUser, Guid> userRepository,
            IGroupRepository groupRepository)
        {
            _groupSubscriptionRepository = groupSubscriptionRepository;
            _userRepository = userRepository;
            _groupRepository = groupRepository;
        }

        /// <summary>
        /// Creating new group subscription in a controlled way.
        /// </summary>
        /// <param name="creatorId"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public async Task<GroupSubscription> CreateAsync(
            [NotNull] Guid creatorId,
            [NotNull] Guid groupId)
        {
            await checkIfCreatorExistsAsync(creatorId);
            await checkIfGroupIsExistsAsync(groupId);
            await disallowMultipleSubscriptionsAsync(creatorId, groupId);

            return new GroupSubscription(
               GuidGenerator.Create(),
               creatorId,
               groupId
           );
        }

        /// <summary>
        /// Checks if a user exists.
        /// </summary>
        /// <param name="creatorId"></param>
        /// <returns></returns>
        /// <exception cref="UserDoesntExistsException"></exception>
        protected async Task checkIfCreatorExistsAsync([NotNull] Guid creatorId)
        {
            Check.NotNull(creatorId, nameof(creatorId));
            var existingUser = await _userRepository.FindAsync(creatorId);
            if (existingUser == null) throw new UserDoesntExistsException(creatorId);
        }

        /// <summary>
        /// Checks if a group exists.
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        /// <exception cref="GroupDoesntExistsException"></exception>
        private async Task checkIfGroupIsExistsAsync([NotNull] Guid groupId)
        {
            Check.NotNull(groupId, nameof(groupId));
            var existingGroup = await _groupRepository.FindByIdAsync(groupId);
            if (existingGroup == null) throw new GroupDoesntExistsException(groupId);
        }

        /// <summary>
        /// Throws an exception when user trying to subscribe to the same group.
        /// </summary>
        /// <param name="creatorId"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        /// <exception cref="SubscriptionAlreadyExistsException"></exception>
        protected async Task disallowMultipleSubscriptionsAsync(
            [NotNull] Guid creatorId,
            [NotNull] Guid groupId)
        {
            var existingGroupSubscription = await checkIfSubscriptionNotExistsAsync(creatorId, groupId);
            if (existingGroupSubscription != null) throw new SubscriptionAlreadyExistsException();
        }

        public async Task<bool> CheckIfUserNotSubscribedAsync(
            [NotNull] Guid creatorId,
            [NotNull] Guid groupId)
        {
            var existingGroupSubscription = await checkIfSubscriptionNotExistsAsync(creatorId, groupId);
            if (existingGroupSubscription != null) return false;
            return true;
        }

        /// <summary>
        /// Checks if the user is subscribed to the group.
        /// </summary>
        /// <param name="creatorId"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        private async Task<GroupSubscription> checkIfSubscriptionNotExistsAsync(
            [NotNull] Guid creatorId,
            [NotNull] Guid groupId)
        {
            Check.NotNull(creatorId, nameof(creatorId));
            Check.NotNull(creatorId, nameof(groupId));
            var subscriptionsList = await _groupSubscriptionRepository.GetListAsync();
            var userGroupSubscriptions = subscriptionsList.Where(subscription => subscription.CreatorId == creatorId);
            return userGroupSubscriptions.FirstOrDefault(userGroupSubscription => userGroupSubscription.GroupId == groupId);
        }
    }
}
