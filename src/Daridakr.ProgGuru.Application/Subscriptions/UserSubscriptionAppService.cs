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
    public class UserSubscriptionAppService : ProgGuruAppService, IUserSubscriptionAppService
    {
        private readonly IUserSubscriptionRepository _userSubscriptionRepository;
        private readonly IIdentityUserRepository _userRepository;

        private readonly UserSubscriptionManager _userSubscriptionManager;

        public UserSubscriptionAppService(
            IUserSubscriptionRepository userSubscriptionRepository,
            IIdentityUserRepository userRepository,
            UserSubscriptionManager userSubscriptionManager)
        {
            _userSubscriptionRepository = userSubscriptionRepository;
            _userRepository = userRepository;
            _userSubscriptionManager = userSubscriptionManager;
        }

        /// <summary>
        /// Get user subscribers.
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserSubscriptionDto>> GetUserSubscribersAsync()
        {
            var userSubscriptions = await _userSubscriptionRepository.GetListAsync();

            var query = from userSubscription in userSubscriptions
                        join @user in await _userRepository.GetListAsync() on userSubscription.CreatorId equals @user.Id
                        select new { userSubscription, @user };

            var userSubscriberDtos = query.Select(x =>
            {
                var userSubscriberDto = ObjectMapper.Map<UserSubscription, UserSubscriptionDto>(x.userSubscription);

                userSubscriberDto.UserName = $" {x.user.Name} {x.user.Surname}";
                userSubscriberDto.UserUsername = x.user.UserName;
                userSubscriberDto.UserProfilePicture = (string)x.user.ExtraProperties["ProfilePictureUrl"];

                return userSubscriberDto;
            }).ToList();

            return userSubscriberDtos;
        }

        /// <summary>
        /// Get user subscriptions.
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserSubscriptionDto>> GetUserSubscriptionsAsync()
        {
            var userSubscriptions = await _userSubscriptionRepository.GetListAsync();

            var query = from userSubscription in userSubscriptions
                        join @user in await _userRepository.GetListAsync() on userSubscription.UserId equals @user.Id
                        select new { userSubscription, @user };

            var userSubscriberDtos = query.Select(x =>
            {
                var userSubscriberDto = ObjectMapper.Map<UserSubscription, UserSubscriptionDto>(x.userSubscription);

                userSubscriberDto.UserName = $" {x.user.Name} {x.user.Surname}";
                userSubscriberDto.UserUsername = x.user.UserName;
                userSubscriberDto.UserProfilePicture = (string)x.user.ExtraProperties["ProfilePictureUrl"];

                return userSubscriberDto;
            }).ToList();

            return userSubscriberDtos;
        }

        /// <summary>
        /// Get all user to user subscriptions for administration.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<AdminUserSubscriptionDto>> GetListAsync(GetUserSubscriptionListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace()) input.Sorting = nameof(UserSubscription.CreationTime);

            var userubscriptions = await _userSubscriptionRepository.GetListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting
            );

            var query = from userSubscription in userubscriptions
                        join creatorUser in await _userRepository.GetListAsync() on userSubscription.CreatorId equals creatorUser.Id
                        join @user in await _userRepository.GetListAsync() on userSubscription.UserId equals @user.Id
                        select new { userSubscription, creatorUser, @user };

            var userSubscriptionDtos = query.Select(x =>
            {
                var userSubscriptionDto = ObjectMapper.Map<UserSubscription, AdminUserSubscriptionDto>(x.userSubscription);

                userSubscriptionDto.CreatorUsername = x.creatorUser.UserName;
                userSubscriptionDto.UserUsername = x.user.UserName;

                return userSubscriptionDto;
            }).ToList();

            var totalCount = await _userSubscriptionRepository.CountAsync();

            return new PagedResultDto<AdminUserSubscriptionDto>(
                totalCount,
                userSubscriptionDtos
            );
        }

        public async Task<bool> TrySubscribeAsync(Guid creatorId, Guid userId)
        {
            return await _userSubscriptionManager.CheckIfUserNotSubscribedAsync(
               creatorId,
               userId
            );
        }

        public async Task SubscribeAsync(Guid creatorId, Guid userId)
        {
            var userSubscription = await _userSubscriptionManager.CreateAsync(
               creatorId,
               userId
            );

            await _userSubscriptionRepository.InsertAsync(userSubscription);
        }

        public async Task UnsubscribeAsync(Guid creatorId, Guid userId)
        {
            var userSubscription = await _userSubscriptionRepository.GetAsync(sub => sub.CreatorId == creatorId && sub.UserId == userId);
            await _userSubscriptionRepository.DeleteAsync(userSubscription.Id);
        }
    }
}
