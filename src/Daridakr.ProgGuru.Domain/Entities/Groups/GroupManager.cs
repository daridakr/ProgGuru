using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Daridakr.ProgGuru.Entities.Base;
using Daridakr.ProgGuru.Entities.Groups.Exceptions;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace Daridakr.ProgGuru.Entities.Groups
{
    // Service to be able to force the unique name constraint
    /// <summary>
    /// Domain service class, that uses for the correct creation of a group, that meets all the requirements of business-logic.
    /// </summary>
    public class GroupManager : InformingEntityManager
    {
        private readonly IGroupRepository _groupRepository;

        public GroupManager(
            IRepository<IdentityUser, Guid> userRepository,
            IGroupRepository groupRepository)
            : base(userRepository)
        {
            _groupRepository = groupRepository;
        }

        /// <summary>
        /// Creating new valid group in a controlled way.
        /// </summary>
        /// <param name="creatorId"></param>
        /// <param name="title"></param>
        /// <param name="subtitle"></param>
        /// <param name="textInformation"></param>
        /// <param name="developer"></param>
        /// <param name="issueYear"></param>
        /// <param name="coverImagePath"></param>
        /// <param name="website"></param>
        /// <returns></returns>
        public async Task<Group> CreateAsync(
            [NotNull] Guid creatorId,
            [NotNull] string title,
            [NotNull] string subtitle,
            [NotNull] string textInformation,
            [NotNull] string developer,
            [NotNull] int issueYear,
            [NotNull] string coverImagePath,
            [CanBeNull] string website = null)
        {
            await checkIfGroupIsExistsAsync(title);
            await TryCreateInformingAsync(creatorId, title, subtitle, textInformation);

            checkIfDeveloperCorrect(developer);
            checkIfWebsiteCorrect(website);
            checkIfIssueYearCorrect(issueYear);
            checkIfCoverImagePathCorrect(coverImagePath);

            return new Group(
                GuidGenerator.Create(),
                creatorId,
                title,
                subtitle,
                textInformation,
                developer,
                issueYear,
                coverImagePath,
                website
            );
        }

        /// <summary>
        /// Checks if a group exists.
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        /// <exception cref="GroupAlreadyExistsException"></exception>
        private async Task checkIfGroupIsExistsAsync([NotNull] string title)
        {
            var existingGroup = await _groupRepository.FindByTitleAsync(title);
            if (existingGroup != null) throw new GroupAlreadyExistsException(title);
        }

        /// <summary>
        /// Checks if a developer correct.
        /// </summary>
        /// <param name="developer"></param>
        private void checkIfDeveloperCorrect([NotNull] string developer)
        {
            Check.NotNullOrWhiteSpace(developer, nameof(developer));
        }

        /// <summary>
        /// Checks if a website correct.
        /// </summary>
        /// <param name="website"></param>
        /// <exception cref="GroupWebsiteInvalidException"></exception>
        private void checkIfWebsiteCorrect([CanBeNull] string website)
        {
            if(!website.IsNullOrEmpty())
            {
                Uri uri;
                if (!Uri.TryCreate(website, UriKind.Absolute, out uri))
                    throw new GroupWebsiteInvalidException(website);
            }
        }

        /// <summary>
        /// Checks if a issue year correct.
        /// </summary>
        /// <param name="issueYear"></param>
        /// <exception cref="GroupIssueYearInvalidException"></exception>
        private void checkIfIssueYearCorrect([NotNull] int issueYear)
        {
            Check.NotNull(issueYear, nameof(issueYear));
            if (issueYear < 1843 || issueYear > DateTime.Now.Year) throw new GroupIssueYearInvalidException();
        }

        /// <summary>
        /// Checks if a cover image path correct.
        /// </summary>
        /// <param name="coverImagePath"></param>
        private void checkIfCoverImagePathCorrect([NotNull] string coverImagePath)
        {
            Check.NotNullOrWhiteSpace(coverImagePath, nameof(coverImagePath));
        }

        /// <summary>
        /// Updating group in a controlled way.
        /// </summary>
        /// <param name="group"></param>
        /// <param name="title"></param>
        /// <param name="subtitle"></param>
        /// <param name="textInformation"></param>
        /// <param name="developer"></param>
        /// <param name="issueYear"></param>
        /// <param name="coverImagePath"></param>
        /// <param name="website"></param>
        /// <returns></returns>
        public async Task<Group> UpdateAsync(
            [NotNull] Group group,
            [NotNull] string title,
            [NotNull] string subtitle,
            [NotNull] string textInformation,
            [NotNull] string developer,
            [NotNull] int issueYear,
            [NotNull] string coverImagePath,
            [CanBeNull] string website = null)
        {
            Check.NotNull(group, nameof(group));
            await ChangeTitleAsync(group, title);
            TryUpdateInforming(group, title, subtitle, textInformation);

            ChangeDeveloper(group, developer);
            ChangeWebsite(group, website);
            ChangeIssueYear(group, issueYear);
            ChangeCoverImagePath(group, coverImagePath);

            return group;
        }

        /// <summary>
        /// Set new title for group.
        /// </summary>
        /// <param name="group"></param>
        /// <param name="newTitle"></param>
        /// <returns></returns>
        /// <exception cref="GroupAlreadyExistsException"></exception>
        public async Task ChangeTitleAsync(
            [NotNull] Group group,
            [NotNull] string newTitle)
        {
            Check.NotNull(group, nameof(group));
            Check.NotNullOrWhiteSpace(newTitle, nameof(newTitle));

            var existingTitle = await _groupRepository.FindByTitleAsync(newTitle);
            if (existingTitle != null && existingTitle.Id != group.Id)
            {
                throw new GroupAlreadyExistsException(newTitle);
            }

            group.ChangeTitle(newTitle);
        }

        /// <summary>
        /// Set new developer for group.
        /// </summary>
        /// <param name="group"></param>
        /// <param name="newDeveloper"></param>
        /// <returns></returns>
        public void ChangeDeveloper([NotNull] Group group, [NotNull] string newDeveloper)
        {
            Check.NotNull(group, nameof(group));
            checkIfDeveloperCorrect(newDeveloper);
            group.ChangeDeveloper(newDeveloper);
        }

        /// <summary>
        /// Set new website for group.
        /// </summary>
        /// <param name="group"></param>
        /// <param name="newWebsite"></param>
        /// <returns></returns>
        public void ChangeWebsite(
            [NotNull] Group group,
            [CanBeNull] string newWebsite)
        {
            Check.NotNull(group, nameof(group));
            checkIfWebsiteCorrect(newWebsite);
            group.ChangeWebsite(newWebsite);
        }

        /// <summary>
        /// Set new issue year for group.
        /// </summary>
        /// <param name="group"></param>
        /// <param name="newIssueYear"></param>
        public void ChangeIssueYear([NotNull] Group group, [NotNull] int newIssueYear)
        {
            Check.NotNull(group, nameof(group));
            checkIfIssueYearCorrect(newIssueYear);
            group.ChangeIssueYear(newIssueYear);
        }

        /// <summary>
        /// Set new cover image path for group.
        /// </summary>
        /// <param name="group"></param>
        /// <param name="newCoverImagePath"></param>
        public void ChangeCoverImagePath(
            [NotNull] Group group,
            [NotNull] string newCoverImagePath)
        {
            Check.NotNull(group, nameof(group));
            Check.NotNullOrWhiteSpace(newCoverImagePath, nameof(newCoverImagePath));
            group.ChangeCoverImagePath(newCoverImagePath);
        }
    }
}
