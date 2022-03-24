using Daridakr.ProgGuru.Entities.Base;
using Daridakr.ProgGuru.Enums.Projects;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp;

namespace Daridakr.ProgGuru.Entities.Projects
{
    /// <summary>
    /// Published user projects, which is a informative post entity that can have articles as news, other posts.
    /// </summary>
    public class Project : PostEntity
    {
        /* CreatorId, Title, Subtitle, TextInformation, Views, Likes, Comments, SavesAmount, CoverImagePath */

        public Guid GroupId { get; private set; }

        /// <summary>
        /// Represents the type of program being developed.
        /// </summary>
        public ProjectCategory Category { get; private set; }

        /// <summary>
        /// The period in which the project is currently located.
        /// </summary>
        public ProjectStatus Status { get; private set; }

        /// <summary>
        /// First publication date of the project.
        /// </summary>
        public DateTime? PublishDate { get; private set; }

        /// <summary>
        /// Official release date of the project for use.
        /// </summary>
        public DateTime? RealeseDate { get; private set; }

        /// <summary>
        /// Link to project for use.
        /// </summary>
        public string GoToUseLink { get; private set; }

        /// <summary>
        /// Link to project, published on GitHub.
        /// </summary>
        public string GoToGitLink { get; private set; }

        ///// <summary>
        ///// User-added links to files with project-code from the him repository on GitHub.
        ///// </summary>
        //public List<RepoCodeFilesLink> RepoCodeFilesLinks { get; private set; }

        //команда (for best times) 

        /******* Initialization ********************************************************************************/

        /// <summary>
        /// This constructor is used by the ORM/database provider.
        /// </summary>
        protected Project() : base() { }

        /// <summary>
        /// This constructor is used for creating new project.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="creatorId"></param>
        /// <param name="groupId"></param>
        /// <param name="title"></param>
        /// <param name="subtitle"></param>
        /// <param name="textInformation"></param>
        /// <param name="coverImagePath"></param>
        /// <param name="category"></param>
        /// <param name="status"></param>
        /// <param name="publishDate"></param>
        /// <param name="realeseDate"></param>
        /// <param name="goToUseLink"></param>
        /// <param name="goToGitLink"></param>
        public Project(
            [NotNull] Guid id,
            [NotNull] Guid creatorId,
            [NotNull] Guid groupId,
            [NotNull] string title,
            [NotNull] string subtitle,
            [NotNull] string textInformation,
            [NotNull] string coverImagePath,
            [NotNull] ProjectCategory category,
            [NotNull] ProjectStatus status,
            [NotNull] DateTime publishDate,
            [CanBeNull] DateTime? realeseDate = null,
            [CanBeNull] string goToUseLink = "",
            [CanBeNull] string goToGitLink = "")
             : base(id, creatorId, title, subtitle, textInformation, coverImagePath)
        {
            SetGroup(groupId);
            SetCategory(category);
            SetStatus(status);
            AverageRating = 0.00f;

            PublishDate = publishDate;
            RealeseDate = realeseDate;
            GoToUseLink = goToUseLink;
            GoToGitLink = goToGitLink;

            //RepoCodeFilesLinks = new List<RepoCodeFilesLink>();
        }

        /// <summary>
        /// Correct project group initialize.
        /// </summary>
        /// <param name="groupId"></param>
        protected void SetGroup([NotNull] Guid groupId)
        {
            GroupId = Check.NotNull(groupId, nameof(groupId));
        }

        /// <summary>
        /// Correct category initialize.
        /// </summary>
        /// <param name="category"></param>
        protected void SetCategory([NotNull] ProjectCategory category)
        {
            Category = Check.NotNull(category, nameof(category));
        }

        /// <summary>
        /// Correct status initialize.
        /// </summary>
        /// <param name="status"></param>
        protected void SetStatus([NotNull] ProjectStatus status)
        {
            Status = Check.NotNull(status, nameof(status));
        }

        /// <summary>
        /// Correct project group changing.
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        internal Project ChangeGroup([NotNull] Guid groupId)
        {
            SetGroup(groupId);
            return this;
        }

        /// <summary>
        /// Correct project group changing.
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        internal Project ChangeCreator([NotNull] Guid creatorId)
        {
            SetCreator(creatorId);
            return this;
        }

        /// <summary>
        /// Correct project category changing.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        internal Project ChangeCategory([NotNull] ProjectCategory category)
        {
            SetCategory(category);
            return this;
        }

        /// <summary>
        /// Correct project category changing.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        internal Project ChangeStatus([NotNull] ProjectStatus status)
        {
            SetStatus(status);
            return this;
        }

        /// <summary>
        /// Correct project publish date changing.
        /// </summary>
        /// <param name="realeseDate"></param>
        /// <returns></returns>
        internal Project ChangePublishDate([CanBeNull] DateTime? publishDate)
        {
            PublishDate = publishDate;
            return this;
        }

        /// <summary>
        /// Correct project realese date changing.
        /// </summary>
        /// <param name="realeseDate"></param>
        /// <returns></returns>
        internal Project ChangeRealeseDate([CanBeNull] DateTime? realeseDate)
        {
            RealeseDate = realeseDate;
            return this;
        }

        /// <summary>
        /// Correct project use link changing.
        /// </summary>
        /// <param name="goToUseLink"></param>
        /// <returns></returns>
        internal Project ChangeGoToUseLink(string goToUseLink)
        {
            GoToUseLink = goToUseLink;
            return this;
        }

        /// <summary>
        /// Correct project git link changing.
        /// </summary>
        /// <param name="goToGitLink"></param>
        /// <returns></returns>
        internal Project ChangeGoToGitLink(string goToGitLink)
        {
            GoToGitLink = goToGitLink;
            return this;
        }
    }
}
