using System;
using System.Threading.Tasks;
using Daridakr.ProgGuru.Entities.Base;
using Daridakr.ProgGuru.Entities.Groups;
using Daridakr.ProgGuru.Entities.Groups.Exceptions;
using Daridakr.ProgGuru.Entities.Projects.Exceptions;
using Daridakr.ProgGuru.Enums.Projects;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace Daridakr.ProgGuru.Entities.Projects
{
    /// <summary>
    /// Domain service class, that uses for the correct creation of a project, that meets all the requirements of business-logic.
    /// </summary>
    public class ProjectManager : PostManager
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IGroupRepository _groupRepository;

        public ProjectManager(
             IProjectRepository projectRepository,
             IGroupRepository groupRepository,
             IRepository<IdentityUser, Guid> userRepository)
            : base(userRepository)
        {
            _projectRepository = projectRepository;
            _groupRepository = groupRepository;
        }

        /// <summary>
        /// Creating new valid project in a controlled way.
        /// </summary>
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
        /// <returns></returns>
        public async Task<Project> CreateAsync(
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
        {
            await TryCreatePostAsync(creatorId, title, subtitle, textInformation, coverImagePath);

            await checkIfGroupIsExistsAsync(groupId);
            Check.NotNull(category, nameof(category));
            checkIfProjectStatusValid(status, publishDate, realeseDate);
            await checkIfProjectGitLinkIsCorrectAsync(goToGitLink, creatorId);
            await checkIfProjectLinkIsCorrectAsync(goToUseLink, creatorId);

            return new Project(
                GuidGenerator.Create(),
                creatorId,
                groupId,
                title,
                subtitle,
                textInformation,
                coverImagePath,
                category,
                status,
                publishDate,
                realeseDate,
                goToUseLink,
                goToGitLink
            );
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
        /// Checks if the project status matches the publish and realese dates.
        /// </summary>
        /// <param name="projectStatus"></param>
        /// <param name="publishDate"></param>
        /// <param name="realeseDate"></param>
        /// <exception cref="CompletedProjectRequiresException"></exception>
        /// <exception cref="CompletedProjectInvalidPublishDateException"></exception>
        /// <exception cref="CompletedProjectPublishDateLaterReleaseDateException"></exception>
        /// <exception cref="AbandonedProjectRequiresPublishDateException"></exception>
        /// <exception cref="AbandonedProjectInvalidPublishDateException"></exception>
        /// <exception cref="AbandonedProjectShouldNotHaveReleaseDateException"></exception>
        /// <exception cref="DuringProjectRequiresRealeseDateException"></exception>
        /// <exception cref="DuringProjectInvalidRealeseDateException"></exception>
        /// <exception cref="DuringProjectInvalidPublishDateException"></exception>
        private void checkIfProjectStatusValid([NotNull] ProjectStatus projectStatus, DateTime? publishDate, DateTime? realeseDate)
        {
            Check.NotNull(projectStatus, nameof(projectStatus));
            switch (projectStatus)
            {
                case ProjectStatus.Completed:
                    if (publishDate == null) throw new CompletedProjectRequiresException(nameof(publishDate));
                    if (realeseDate == null) throw new CompletedProjectRequiresException(nameof(realeseDate));
                    if (publishDate != null && realeseDate != null)
                    {
                        var today = DateTime.Now;
                        if (publishDate > today) throw new CompletedProjectInvalidPublishDateException();
                        if (publishDate > realeseDate) throw new CompletedProjectPublishDateLaterReleaseDateException();
                    }
                    break;
                case ProjectStatus.Abandoned:
                    if (publishDate == null) throw new AbandonedProjectRequiresPublishDateException();
                    else if (publishDate > DateTime.Now) throw new AbandonedProjectInvalidPublishDateException();
                    if (realeseDate != null) throw new AbandonedProjectShouldNotHaveReleaseDateException();
                    break;
                case ProjectStatus.During:
                    if (realeseDate == null) throw new DuringProjectRequiresRealeseDateException();
                    else if (realeseDate < DateTime.Now) throw new DuringProjectInvalidRealeseDateException();
                    if (publishDate != null) { if (publishDate > DateTime.Now) throw new DuringProjectInvalidPublishDateException(); }
                    break;
            }
        }

        /// <summary>
        /// Checks if the project github link is correct.
        /// </summary>
        /// <param name="gitLink"></param>
        /// <param name="creatorId"></param>
        /// <returns></returns>
        /// <exception cref="NotGithubLinkForProjectException"></exception>
        /// <exception cref="ProjectWithTheSameRepositoryAlreadyExistsException"></exception>
        private async Task checkIfProjectGitLinkIsCorrectAsync([CanBeNull] string gitLink, Guid? creatorId)
        {
            // if goToGitLink was get
            if (!gitLink.IsNullOrEmpty())
            {
                // check for github.com link
                if (!gitLink.Contains("github.com")) throw new NotGithubLinkForProjectException();
                // check for the same goToGit links
                var existingRepository = await _projectRepository.FindByGitHubLinksAmongUserProjectsAsync(gitLink, creatorId);
                if (existingRepository != null) throw new ProjectWithTheSameRepositoryAlreadyExistsException();
            }
        }

        /// <summary>
        /// Checks if the project use link is correct.
        /// </summary>
        /// <param name="goToUseLink"></param>
        /// <param name="creatorId"></param>
        /// <returns></returns>
        /// <exception cref="ProjectAlreadyExistsException"></exception>
        private async Task checkIfProjectLinkIsCorrectAsync([CanBeNull] string goToUseLink, Guid? creatorId)
        {
            // if goToUseLink was get
            if (!goToUseLink.IsNullOrEmpty())
            {
                // check for the same goToUse links
                var existingProject = await _projectRepository.FindByGoToUseLinksAmongUserProjectsAsync(goToUseLink, creatorId);
                if (existingProject != null) throw new ProjectAlreadyExistsException();
            }
        }

        /// <summary>
        /// Updating project in a controlled way.
        /// </summary>
        /// <param name="project"></param>
        /// <param name="groupId"></param>
        /// <param name="title"></param>
        /// <param name="subtitle"></param>
        /// <param name="textInformation"></param>
        /// <param name="coverImagePath"></param>
        /// <param name="category"></param>
        /// <param name="status"></param>
        /// <param name="realeseDate"></param>
        /// <param name="goToUseLink"></param>
        /// <param name="goToGitLink"></param>
        /// <returns></returns>
        public async Task<Project> UpdateAsync(
            [NotNull] Project project,
            [NotNull] Guid groupId,
            [NotNull] string title,
            [NotNull] string subtitle,
            [NotNull] string textInformation,
            [NotNull] string coverImagePath,
            [NotNull] ProjectCategory category,
            [NotNull] ProjectStatus status,
            [CanBeNull] DateTime? realeseDate = null,
            [CanBeNull] string goToUseLink = "",
            [CanBeNull] string goToGitLink = "")
        {
            Check.NotNull(project, nameof(project));
            TryUpdatePost(project, title, subtitle, textInformation, coverImagePath);

            await ChangeGroupAsync(project, groupId);
            ChangeCategory(project, category);
            ChangeStatus(project, status, project.PublishDate, realeseDate);
            ChangeRealeseDate(project, realeseDate);
            await ChangeGoToUseLink(project, goToUseLink, project.CreatorId);
            await ChangeGoToGitLink(project, goToGitLink, project.CreatorId);

            return project;
        }

        /// <summary>
        /// Admin updating project in a controlled way.
        /// </summary>
        /// <param name="project"></param>
        /// <param name="creatorId"></param>
        /// <param name="groupId"></param>
        /// <param name="title"></param>
        /// <param name="subtitle"></param>
        /// <param name="textInformation"></param>
        /// <param name="coverImagePath"></param>
        /// <param name="category"></param>
        /// <param name="status"></param>
        /// <param name="realeseDate"></param>
        /// <param name="goToUseLink"></param>
        /// <param name="goToGitLink"></param>
        /// <returns></returns>
        public async Task<Project> AdminUpdateAsync(
            [NotNull] Project project,
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
        {
            project = await UpdateAsync(project, groupId, title, subtitle, textInformation, coverImagePath, category, status, realeseDate, goToUseLink, goToGitLink);

            await ChangeCreatorAsync(project, creatorId);
            ChangePublishDate(project, publishDate);

            return project;
        }

        /// <summary>
        /// Set new group for project.
        /// </summary>
        /// <param name="project"></param>
        /// <param name="newGroupId"></param>
        /// <returns></returns>
        public async Task ChangeGroupAsync([NotNull] Project project, [NotNull] Guid newGroupId)
        {
            Check.NotNull(project, nameof(project));
            await checkIfGroupIsExistsAsync(newGroupId);
            project.ChangeGroup(newGroupId);
        }

        /// <summary>
        /// Set new creator for project.
        /// </summary>
        /// <param name="project"></param>
        /// <param name="newCreatorId"></param>
        /// <returns></returns>
        private async Task ChangeCreatorAsync([NotNull] Project project, [NotNull] Guid newCreatorId)
        {
            Check.NotNull(project, nameof(project));
            await checkIfCreatorExistsAsync(newCreatorId);
            project.ChangeCreator(newCreatorId);
        }

        /// <summary>
        /// Set new category for project.
        /// </summary>
        /// <param name="project"></param>
        /// <param name="newCategory"></param>
        public void ChangeCategory(
            [NotNull] Project project,
            [NotNull] ProjectCategory newCategory)
        {
            Check.NotNull(project, nameof(project));
            Check.NotNull(newCategory, nameof(newCategory));
            project.ChangeCategory(newCategory);
        }

        /// <summary>
        /// Set new status for project.
        /// </summary>
        /// <param name="project"></param>
        /// <param name="newStatus"></param>
        /// <param name="publishDate"></param>
        /// <param name="realeseDate"></param>
        public void ChangeStatus(
            [NotNull] Project project,
            [NotNull] ProjectStatus newStatus,
            DateTime? publishDate,
            DateTime? realeseDate)
        {
            Check.NotNull(project, nameof(project));
            Check.NotNull(newStatus, nameof(newStatus));
            checkIfProjectStatusValid(newStatus, publishDate, realeseDate);
            project.ChangeStatus(newStatus);
        }

        /// <summary>
        /// Set new publish date for project.
        /// </summary>
        /// <param name="project"></param>
        /// <param name="newPublishDate"></param>
        public void ChangePublishDate(
            [NotNull] Project project,
            [CanBeNull] DateTime? newPublishDate)
        {
            Check.NotNull(project, nameof(project));
            project.ChangePublishDate(newPublishDate);
        }

        /// <summary>
        /// Set new realese date for project.
        /// </summary>
        /// <param name="project"></param>
        /// <param name="newRealeseDate"></param>
        public void ChangeRealeseDate(
            [NotNull] Project project,
            [CanBeNull] DateTime? newRealeseDate)
        {
            Check.NotNull(project, nameof(project));
            project.ChangeRealeseDate(newRealeseDate);
        }

        /// <summary>
        /// Set new use link for project.
        /// </summary>
        /// <param name="project"></param>
        /// <param name="newGoToUseLink"></param>
        public async Task ChangeGoToUseLink(
            [NotNull] Project project,
            [CanBeNull] string newGoToUseLink,
            Guid? creatorId)
        {
            Check.NotNull(project, nameof(project));
            await checkIfProjectLinkIsCorrectAsync(newGoToUseLink, creatorId);
            project.ChangeGoToUseLink(newGoToUseLink);
        }

        /// <summary>
        /// Set new git link for project.
        /// </summary>
        /// <param name="project"></param>
        /// <param name="goToGitLink"></param>
        public async Task ChangeGoToGitLink(
            [NotNull] Project project,
            [CanBeNull] string goToGitLink,
            Guid? creatorId)
        {
            Check.NotNull(project, nameof(project));
            await checkIfProjectGitLinkIsCorrectAsync(goToGitLink, creatorId);
            project.ChangeGoToGitLink(goToGitLink);
        }
    }
}
