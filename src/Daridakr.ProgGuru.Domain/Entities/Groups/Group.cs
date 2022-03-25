using System;
using Daridakr.ProgGuru.Consts;
using Daridakr.ProgGuru.Entities.Base;
using JetBrains.Annotations;
using Volo.Abp;

namespace Daridakr.ProgGuru.Entities.Groups
{
    public class Group : InformingEntity
    {
        /* Title, Subtitle, TextInformation, Creator */

        public string Developer { get; protected set; }

        public string Website { get; protected set; }

        public int IssueYear { get; protected set; }

        public string CoverImagePath { get; protected set; }

        // статьи

        /******* Initialization ********************************************************************************/

        /// <summary>
        /// This constructor is used by the ORM/database provider.
        /// </summary>
        protected Group() : base() { }

        /// <summary>
        /// This constructor is used for creating new group.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="creatorId"></param>
        /// <param name="title"></param>
        /// <param name="subtitle"></param>
        /// <param name="textInformation"></param>
        /// <param name="developer"></param>
        /// <param name="issueYear"></param>
        /// <param name="coverImagePath"></param>
        /// <param name="website"></param>
        internal Group(
            [NotNull] Guid id,
            [NotNull] Guid creatorId,
            [NotNull] string title,
            [NotNull] string subtitle,
            [NotNull] string textInformation,
            [NotNull] string developer,
            [NotNull] int issueYear,
            [NotNull] string coverImagePath,
            [CanBeNull] string website = null)
             : base(id, creatorId, title, subtitle, textInformation)
        {
            SetDeveloper(developer);
            SetWebsite(website);
            SetIssueYear(issueYear);
            SetCoverImagePath(coverImagePath);
        }

        /// <summary>
        /// Method for correct group title initialize.
        /// </summary>
        /// <param name="title"></param>
        protected override void SetTitle([NotNull] string title)
        {
            // it throws ArgumentException on an invalid case
            Title = Check.NotNullOrWhiteSpace(
                title,
                nameof(title),
                maxLength: GroupConsts.MaxTitleLength
            );
        }

        /// <summary>
        /// Correct group subtitle initialize.
        /// </summary>
        /// <param name="subtitle"></param>
        protected override void SetSubtitle([NotNull] string subtitle)
        {
            Subtitle = Check.NotNullOrWhiteSpace(
                subtitle,
                nameof(subtitle),
                maxLength: GroupConsts.MaxSubtitleLength
            );
        }

        /// <summary>
        /// Correct group information initialize.
        /// </summary>
        /// <param name="textInformation"></param>
        protected override void SetTextInformation([NotNull] string textInformation)
        {
            TextInformation = Check.NotNullOrWhiteSpace(
                textInformation,
                nameof(textInformation),
                maxLength: GroupConsts.MaxTextInformationLength
            );
        }

        /// <summary>
        /// Correct group developer initialize.
        /// </summary>
        /// <param name="developer"></param>
        protected void SetDeveloper([NotNull] string developer)
        {
            Developer = Check.NotNullOrWhiteSpace(
                developer,
                nameof(developer),
                maxLength: GroupConsts.MaxDeveloperLength
            );
        }

        /// <summary>
        /// Correct group website initialize.
        /// </summary>
        /// <param name="website"></param>
        protected void SetWebsite([CanBeNull] string website)
        {
            if (website != null)
            {
                Website = Check.NotNullOrWhiteSpace(
                website,
                nameof(website),
                maxLength: GroupConsts.MaxWebsiteLength
                );
            }
            else Website = website;
        }

        /// <summary>
        /// Correct group issue year initialize.
        /// </summary>
        /// <param name="issueYear"></param>
        protected void SetIssueYear([NotNull] int issueYear)
        {
            IssueYear = Check.NotNull(issueYear, nameof(issueYear));
        }

        /// <summary>
        /// Correct group cover initialize.
        /// </summary>
        /// <param name="coverImagePath"></param>
        protected void SetCoverImagePath([NotNull] string coverImagePath)
        {
            CoverImagePath = Check.NotNullOrWhiteSpace(coverImagePath, nameof(coverImagePath));
        }

        /// <summary>
        /// Correct group developer changing.
        /// </summary>
        /// <param name="developer"></param>
        /// <returns></returns>
        internal Group ChangeDeveloper([NotNull] string developer)
        {
            SetDeveloper(developer);
            return this;
        }

        /// <summary>
        /// Correct group website changing.
        /// </summary>
        /// <param name="website"></param>
        /// <returns></returns>
        internal Group ChangeWebsite([CanBeNull] string website)
        {
            SetWebsite(website);
            return this;
        }

        /// <summary>
        /// Correct group issue year changing.
        /// </summary>
        /// <param name="issueYear"></param>
        /// <returns></returns>
        internal Group ChangeIssueYear([NotNull] int issueYear)
        {
            SetIssueYear(issueYear);
            return this;
        }

        /// <summary>
        /// Correct group cover changing.
        /// </summary>
        /// <param name="coverImagePath"></param>
        /// <returns></returns>
        internal Group ChangeCoverImagePath([NotNull] string coverImagePath)
        {
            SetCoverImagePath(coverImagePath);
            return this;
        }
    }
}
