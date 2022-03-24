using Daridakr.ProgGuru.Consts;
using Daridakr.ProgGuru.Entities.Projects;
using Daridakr.ProgGuru.Helpers;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Volo.Abp;

namespace Daridakr.ProgGuru.Entities.Base
{
    /// <summary>
    /// Abstract base class for post entities - project, article and vacancy, etc.
    /// </summary>
    public abstract class PostEntity : InformingEntity
    {
        /* CreatorId, Title, Subtitle, TextInformation */

        /// <summary>
        /// Value indicating the total number of users who viewed this post.
        /// </summary>
        public int Views { get; private set; }

        /// <summary>
        /// Value indicating the number of times this post has been saved by users.
        /// </summary>
        public int SavesAmount { get; private set; }

        /// <summary>
        /// Text field containing a link to the cover of the post.
        /// </summary>
        public string CoverImagePath { get; protected set; }

        /******* Initialization ********************************************************************************/

        /// <summary>
        /// This constructor is used by the ORM/database provider.
        /// </summary>
        protected PostEntity() : base() { }

        /// <summary>
        /// This constructor is used for creating a new post.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="creatorId"></param>
        /// <param name="title"></param>
        /// <param name="subtitle"></param>
        /// <param name="textInformation"></param>
        /// <param name="coverImagePath"></param>
        internal PostEntity(
            [NotNull] Guid id,
            [NotNull] Guid creatorId,
            [NotNull] string title,
            [NotNull] string subtitle,
            [NotNull] string textInformation,
            [NotNull] string coverImagePath)
            : base(id, creatorId, title, subtitle, textInformation)
        {
            Views = 0;
            SavesAmount = 0;
            SetCoverImagePath(coverImagePath);
        }

        /******* Initialization Methods ********************************************************************************/

        /// <summary>
        /// Correct post title initialize.
        /// </summary>
        /// <param name="title"></param>
        protected override void SetTitle([NotNull] string title)
        {
            Check.NotNullOrWhiteSpace(title, nameof(title),
                minLength: PostConsts.MinTitleLength,
                maxLength: PostConsts.MaxTitleLength);
            ProgGuruCheck.IsStartsWithSeparatorChar(title, nameof(title));
            Title = ProgGuruConverter.ConvertToPostTitle(title);
        }

        /// <summary>
        /// Correct project subtitle initialize.
        /// </summary>
        /// <param name="subtitle"></param>
        protected override void SetSubtitle([NotNull] string subtitle)
        {
            Check.NotNullOrWhiteSpace(subtitle, nameof(subtitle), PostConsts.MaxSubtitleLength, PostConsts.MinSubtitleLength);
            Subtitle = ProgGuruCheck.IsStartsWithSeparatorChar(subtitle, nameof(subtitle));
        }

        /// <summary>
        /// Correct project information initialize.
        /// </summary>
        /// <param name="textInformation"></param>
        protected override void SetTextInformation([NotNull] string textInformation)
        {
            TextInformation = Check.NotNullOrWhiteSpace(textInformation, nameof(textInformation), PostConsts.MaxTextInformationLength, PostConsts.MinTextInformationLength);
        }

        /// <summary>
        /// Correct project cover initialize. Override this method to configure your rules for cover initialize.
        /// </summary>
        /// <param name="coverImagePath"></param>
        protected virtual void SetCoverImagePath([NotNull] string coverImagePath)
        {
            CoverImagePath = Check.NotNullOrWhiteSpace(coverImagePath, nameof(coverImagePath));
        }

        /// <summary>
        /// Correct project cover changing.
        /// </summary>
        /// <param name="coverImagePath"></param>
        /// <returns></returns>
        internal PostEntity ChangeCoverImagePath([NotNull] string coverImagePath)
        {
            SetCoverImagePath(coverImagePath);
            return this;
        }

        public virtual PostEntity IncreaseReadCount()
        {
            Views++;
            return this;
        }
    }
}
