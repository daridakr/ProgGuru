using JetBrains.Annotations;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace Daridakr.ProgGuru.Entities.Base
{
    /// <summary>
    /// Base domain service class for post entities, that contains some base business rules for all posts.
    /// </summary>
    public class PostManager : InformingEntityManager
    {
        public PostManager(
             IRepository<IdentityUser, Guid> userRepository)
            : base(userRepository)
        {

        }

        protected async Task TryCreatePostAsync(
             [NotNull] Guid creatorId,
             [NotNull] string title,
             [NotNull] string subtitle,
             [NotNull] string textInformation,
             [NotNull] string coverImagePath)
        {
            await TryCreateInformingAsync(creatorId, title, subtitle, textInformation);
            checkIfCoverImagePathCorrect(coverImagePath);
        }

        /// <summary>
        /// Checks if a cover image path correct.
        /// </summary>
        /// <param name="coverImagePath"></param>
        private void checkIfCoverImagePathCorrect([NotNull] string coverImagePath)
        {
            Check.NotNullOrWhiteSpace(coverImagePath, nameof(coverImagePath));
        }

        protected void TryUpdatePost(
            [NotNull] PostEntity post,
            [NotNull] string title,
            [NotNull] string subtitle,
            [NotNull] string textInformation,
            [NotNull] string coverImagePath)
        {
            Check.NotNull(post, nameof(post));
            TryUpdateInforming(post, title, subtitle, textInformation);
            ChangeCoverImagePath(post, coverImagePath);
        }

        /// <summary>
        /// Set new cover image path for project.
        /// </summary>
        /// <param name="project"></param>
        /// <param name="newCoverImagePath"></param>
        public void ChangeCoverImagePath(
            [NotNull] PostEntity post,
            [NotNull] string newCoverImagePath)
        {
            Check.NotNull(post, nameof(post));
            Check.NotNullOrWhiteSpace(newCoverImagePath, nameof(newCoverImagePath));
            checkIfCoverImagePathCorrect(newCoverImagePath);
            post.ChangeCoverImagePath(newCoverImagePath);
        }
    }
}
