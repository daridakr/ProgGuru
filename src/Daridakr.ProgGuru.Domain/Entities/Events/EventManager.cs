using Daridakr.ProgGuru.Entities.Base;
using JetBrains.Annotations;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace Daridakr.ProgGuru.Entities.Events
{
    public class EventManager : PostManager
    {
        public EventManager(
             IRepository<IdentityUser, Guid> userRepository)
            : base(userRepository)
        {

        }

        /******* Create ********************************************************************************/

        public async Task<Event> CreateAsync(
            [NotNull] Guid creatorId,
            [NotNull] string title,
            [NotNull] string subtitle,
            [NotNull] string textInformation,
            [NotNull] string coverImagePath,
            [NotNull] DateTime startDate,
            [CanBeNull] DateTime? endDate = null,
            [CanBeNull] string[] tagNames = null,
            int cost = 0)
        {
            await TryCreatePostAsync(creatorId, title, subtitle, textInformation, coverImagePath);

            if (endDate != null) checkIfDatesValid(startDate, (DateTime)endDate);

            var @event = new Event(
                GuidGenerator.Create(),
                creatorId,
                title,
                subtitle,
                textInformation,
                coverImagePath,
                startDate,
                endDate,
                cost
            );

            //await SetTagsAsync(@event, tagNames);

            return @event;
        }

        public async Task<Event> UpdateAsync(
            [NotNull] Event @event,
            [NotNull] string title,
            [NotNull] string subtitle,
            [NotNull] string textInformation,
            [NotNull] string coverImagePath,
            [NotNull] DateTime startDate,
            [CanBeNull] DateTime? endDate = null,
            [CanBeNull] string[] tagNames = null,
            int cost = 0)
        {
            Check.NotNull(@event, nameof(@event));
            TryUpdatePost(@event, title, subtitle, textInformation, coverImagePath);
            ChangeDates(@event, startDate, endDate);
            ChangeCost(@event, cost);

            //await SetTagsAsync(@event, tagNames);

            return @event;
        }

        /******* Business-Logic ********************************************************************************/

        private void checkIfDatesValid(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate) throw new Exception("Event Error: Start date can't be more than end date");
        }

        /***********************************************************************************************************************/

        /// <summary>
        /// Set new dates for event.
        /// </summary>
        /// <param name="event"></param>
        /// <param name="newStartDate"></param>
        /// <returns></returns>
        public void ChangeDates(
            [NotNull] Event @event,
            [NotNull] DateTime newStartDate,
            [CanBeNull] DateTime? newEndDate)
        {
            Check.NotNull(@event, nameof(@event));

            if (newEndDate != null) checkIfDatesValid(newStartDate, (DateTime)newEndDate);

            @event.ChangeStartDate(newStartDate);
            @event.ChangeEndDate(newEndDate);
        }

        /// <summary>
        /// Set new cost for event.
        /// </summary>
        /// <param name="event"></param>
        /// <param name="cost"></param>
        public void ChangeCost(
            [NotNull] Event @event,
            int cost)
        {
            Check.NotNull(@event, nameof(@event));
            @event.ChangeCost(cost);
        }
    }
}
