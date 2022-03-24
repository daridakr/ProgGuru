using Daridakr.ProgGuru.Entities.Users.Exceptions;
using JetBrains.Annotations;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Identity;

namespace Daridakr.ProgGuru.Entities.Base
{
    public class InformingEntityManager : DomainService
    {
        private readonly IRepository<IdentityUser, Guid> _userRepository;

        public InformingEntityManager(
             IRepository<IdentityUser, Guid> userRepository)
        {
            _userRepository = userRepository;
        }

        protected async Task TryCreateInformingAsync(
            [NotNull] Guid creatorId,
            [NotNull] string title,
            [NotNull] string subtitle,
            [NotNull] string textInformation)
        {
            await checkIfCreatorExistsAsync(creatorId);
            Check.NotNullOrWhiteSpace(title, nameof(title));
            Check.NotNullOrWhiteSpace(subtitle, nameof(subtitle));
            Check.NotNullOrWhiteSpace(textInformation, nameof(textInformation));
        }

        protected void TryUpdateInforming(
            [NotNull] InformingEntity entity,
            [NotNull] string title,
            [NotNull] string subtitle,
            [NotNull] string textInformation)
        {
            Check.NotNull(entity, nameof(entity));
            ChangeTitle(entity, title);
            ChangeSubtitle(entity, subtitle);
            ChangeTextInformation(entity, textInformation);
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
        /// Set new title for entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="newTitle"></param>
        public void ChangeTitle(
            [NotNull] InformingEntity entity,
            [NotNull] string newTitle)
        {
            Check.NotNull(entity, nameof(entity));
            Check.NotNullOrWhiteSpace(newTitle, nameof(newTitle));
            entity.ChangeTitle(newTitle);
        }

        /// <summary>
        /// Set new subtitle for entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="newSubtitle"></param>
        public void ChangeSubtitle(
            [NotNull] InformingEntity entity,
            [NotNull] string newSubtitle)
        {
            Check.NotNull(entity, nameof(entity));
            Check.NotNullOrWhiteSpace(newSubtitle, nameof(newSubtitle));
            entity.ChangeSubtitle(newSubtitle);
        }

        /// <summary>
        /// Set new text information for entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="newTextInformation"></param>
        public void ChangeTextInformation(
            [NotNull] InformingEntity entity,
            [NotNull] string newTextInformation)
        {
            Check.NotNull(entity, nameof(entity));
            Check.NotNullOrWhiteSpace(newTextInformation, nameof(newTextInformation));
            entity.ChangeTextInformation(newTextInformation);
        }
    }
}
