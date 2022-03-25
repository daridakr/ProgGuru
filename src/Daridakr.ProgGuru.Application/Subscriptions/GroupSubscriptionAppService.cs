using Daridakr.ProgGuru.Entities.Groups;
using Daridakr.ProgGuru.Entities.Subscriptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace Daridakr.ProgGuru.Subscriptions
{
    public class GroupSubscriptionAppService : ProgGuruAppService, IGroupSubscriptionAppService
    {
        private readonly IGroupSubscriptionRepository _groupSubscriptionRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IIdentityUserRepository _userRepository;

        private readonly GroupSubscriptionManager _groupSubscriptionManager;

        public GroupSubscriptionAppService(
            IGroupSubscriptionRepository groupSubscriptionRepository,
            IGroupRepository groupRepository,
            IIdentityUserRepository userRepository,
            GroupSubscriptionManager groupSubscriptionManager)
        {
            _groupSubscriptionRepository = groupSubscriptionRepository;
            _groupRepository = groupRepository;
            _userRepository = userRepository;
            _groupSubscriptionManager = groupSubscriptionManager;
        }

        /// <summary>
        /// Get group subscribers.
        /// </summary>
        /// <returns></returns>
        public async Task<List<GroupSubscriberDto>> GetGroupSubscribersAsync()
        {
            var groupSubscriptions = await _groupSubscriptionRepository.GetListAsync();

            var query = from groupSubscription in groupSubscriptions
                        join @user in await _userRepository.GetListAsync() on groupSubscription.CreatorId equals @user.Id
                        select new { groupSubscription, @user };

            var groupSubscriberDtos = query.Select(x =>
            {
                var groupSubscriberDto = ObjectMapper.Map<GroupSubscription, GroupSubscriberDto>(x.groupSubscription);

                groupSubscriberDto.CreatorName = $" {x.user.Name} {x.user.Surname}";
                groupSubscriberDto.CreatorUserName = x.user.UserName;
                groupSubscriberDto.CreatorProfilePicture = (string)x.user.ExtraProperties["ProfilePictureUrl"];

                return groupSubscriberDto;
            }).ToList();

            return groupSubscriberDtos;
        }

        /// <summary>
        /// Get group subscriptions of user.
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserGroupSubscriptionDto>> GetUserSubscriptionsAsync()
        {
            var groupSubscriptions = await _groupSubscriptionRepository.GetListAsync();

            var query = from groupSubscription in groupSubscriptions
                        join @group in await _groupRepository.GetQueryableAsync() on groupSubscription.GroupId equals @group.Id
                        select new { groupSubscription, @group };

            var userGroupSubscriptionDtos = query.Select(x =>
            {
                var userGroupSubscriptionDto = ObjectMapper.Map<GroupSubscription, UserGroupSubscriptionDto>(x.groupSubscription);

                userGroupSubscriptionDto.GroupTitle = x.group.Title;
                userGroupSubscriptionDto.GroupSubtitle = x.group.Subtitle;
                userGroupSubscriptionDto.GroupCoverImagePath = x.group.CoverImagePath;

                return userGroupSubscriptionDto;
            }).ToList();

            return userGroupSubscriptionDtos;
        }

        /// <summary>
        /// Get all user to group subscriptions for administration.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<AdminGroupSubscriptionDto>> GetListAsync(GetGroupSubscriptionListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace()) input.Sorting = nameof(GroupSubscription.CreationTime);

            var groupSubscriptions = await _groupSubscriptionRepository.GetListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting
            );

            var query = from groupSubscription in groupSubscriptions
                        join @group in await _groupRepository.GetQueryableAsync() on groupSubscription.GroupId equals @group.Id
                        join @user in await _userRepository.GetListAsync() on groupSubscription.CreatorId equals @user.Id
                        select new { groupSubscription, @group, @user };

            var groupSubscriptionDtos = query.Select(x =>
            {
                var groupSubscriptionDto = ObjectMapper.Map<GroupSubscription, AdminGroupSubscriptionDto>(x.groupSubscription);

                groupSubscriptionDto.GroupTitle = x.group.Title;
                groupSubscriptionDto.CreatorUserName = x.user.UserName;

                return groupSubscriptionDto;
            }).ToList();

            var totalCount = await _groupSubscriptionRepository.CountAsync();

            return new PagedResultDto<AdminGroupSubscriptionDto>(
                totalCount,
                groupSubscriptionDtos
            );
        }

        public async Task<bool> TrySubscribeAsync(Guid creatorId, Guid groupId)
        {
            return await _groupSubscriptionManager.CheckIfUserNotSubscribedAsync(
               creatorId,
               groupId
            );
        }

        public async Task SubscribeAsync(Guid creatorId, Guid groupId)
        {
            var groupSubscription = await _groupSubscriptionManager.CreateAsync(
               creatorId,
               groupId
            );

            await _groupSubscriptionRepository.InsertAsync(groupSubscription);
        }

        public async Task UnsubscribeAsync(Guid creatorId, Guid groupId)
        {
            var groupSubscription = await _groupSubscriptionRepository.GetAsync(sub => sub.CreatorId == creatorId && sub.GroupId == groupId);
            await _groupSubscriptionRepository.DeleteAsync(groupSubscription.Id);
        }
    }
}
