using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Daridakr.ProgGuru.CloudStorage;
using Daridakr.ProgGuru.Entities.Groups;
using Daridakr.ProgGuru.Entities.Projects;
using Daridakr.ProgGuru.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.CmsKit.Admin.Blogs;
using Volo.CmsKit.Admin.Tags;
using Volo.CmsKit.Comments;

namespace Daridakr.ProgGuru.Projects
{
    [Authorize(ProgGuruPermissions.Projects.Default)]
    public class ProjectAppService : ProgGuruAppService, IProjectAppService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IIdentityUserRepository _userRepository;
        private readonly ICommentRepository _commentRepository;

        private readonly ProjectManager _projectManager;
        private readonly CloudinaryManager _cloudinaryManager;

        private readonly EntityTagAdminAppService _entityTagAdminAppService;
        private readonly BlogPostAdminAppService _articleAppService;

        public ProjectAppService(
            IProjectRepository projectRepository,
            IGroupRepository groupRepository,
            IIdentityUserRepository userRepository,
            ICommentRepository commentRepository,
            ProjectManager projectManager,
            CloudinaryManager cloudinaryManager,
            EntityTagAdminAppService entityTagAdminAppService,
            BlogPostAdminAppService articleAppService)
        {
            _projectRepository = projectRepository;
            _groupRepository = groupRepository;
            _userRepository = userRepository;
            _commentRepository = commentRepository;
            _projectManager = projectManager;
            _cloudinaryManager = cloudinaryManager;
            _entityTagAdminAppService = entityTagAdminAppService;
            _articleAppService = articleAppService;
        }

        public async Task<ProjectDto> GetAsync(Guid id)
        {
            var project = await _projectRepository.GetAsync(id);
            var group = await _groupRepository.GetAsync(project.GroupId);
            var user = await _userRepository.GetAsync((Guid)project.CreatorId);

            var projectDto = ObjectMapper.Map<Project, ProjectDto>(project);

            projectDto.GroupTitle = group.Title;
            projectDto.CreatorName = $" {user.Name} {user.Surname}";
            projectDto.CreatorUserName = user.UserName;
            projectDto.CreatorProfilePicture = (string)user.ExtraProperties["ProfilePictureUrl"];

            var projectsComments = (await _commentRepository.GetListWithAuthorsAsync("Project", projectDto.Id.ToString()));
            projectDto.CommentCount = projectsComments.Count();

            return projectDto;
        }

        public async Task<PagedResultDto<ProjectDto>> GetListAsync(GetProjectListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace()) input.Sorting = nameof(Project.Title);

            var projects = await _projectRepository.GetListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                input.Filter
            );

            var query = from project in projects
                        join @group in await _groupRepository.GetQueryableAsync() on project.GroupId equals @group.Id
                        join @user in await _userRepository.GetListAsync() on project.CreatorId equals @user.Id
                        select new { project, @group, @user };

            var comments = (await _commentRepository.GetListAsync(null, "Project"));

            var projectDtos = query.Select(x =>
            {
                var projectDto = ObjectMapper.Map<Project, ProjectDto>(x.project);

                projectDto.GroupTitle = x.group.Title;
                projectDto.CreatorName = $" {x.user.Name} {x.user.Surname}";
                projectDto.CreatorUserName = x.user.UserName;
                projectDto.CreatorProfilePicture = (string)x.user.ExtraProperties["ProfilePictureUrl"];
                var dtoComments = comments.Where(c => c.Comment.EntityId == x.project.Id.ToString());
                projectDto.CommentCount = dtoComments.Count();

                return projectDto;
            }).ToList();

            var totalCount = input.Filter == null
                ? await _projectRepository.CountAsync()
                : await _projectRepository.CountAsync(
                project => project.Title.Contains(input.Filter));

            return new PagedResultDto<ProjectDto>(
                totalCount,
                projectDtos
            );
        }

        [Authorize(ProgGuruPermissions.Projects.Create)]
        public async Task<bool> TryCreateAsync(CreateProjectDto input)
        {
            await _projectManager.CreateAsync(
                input.CreatorId,
                input.GroupId,
                input.Title,
                input.Subtitle,
                input.TextInformation,
                input.CoverImagePath,
                input.Category,
                input.Status,
                input.PublishDate,
                input.RealeseDate,
                input.GoToUseLink,
                input.GoToGitLink
            );

            return true;
        }

        [Authorize(ProgGuruPermissions.Projects.Create)]
        public async Task<ProjectDto> CreateAsync(CreateProjectDto input)
        {
            var project = await _projectManager.CreateAsync(
                input.CreatorId,
                input.GroupId,
                input.Title,
                input.Subtitle,
                input.TextInformation,
                input.CoverImagePath,
                input.Category,
                input.Status,
                input.PublishDate,
                input.RealeseDate,
                input.GoToUseLink,
                input.GoToGitLink
            );

            await _projectRepository.InsertAsync(project);

            return ObjectMapper.Map<Project, ProjectDto>(project);
        }

        [Authorize(ProgGuruPermissions.Projects.Edit)]
        public async Task<bool> TryUpdateAsync(Guid id, UpdateProjectDto input)
        {
            var project = await _projectRepository.GetAsync(id);

            await _projectManager.UpdateAsync(
                project,
                input.GroupId,
                input.Title,
                input.Subtitle,
                input.TextInformation,
                input.CoverImagePath,
                input.Category,
                input.Status,
                input.RealeseDate,
                input.GoToUseLink,
                input.GoToGitLink
            );

            return true;
        }

        [Authorize(ProgGuruPermissions.Projects.Edit)]
        public async Task<ProjectDto> UpdateAsync(Guid id, UpdateProjectDto input)
        {
            var project = await _projectRepository.GetAsync(id);

            if (input.CoverImagePath != project.CoverImagePath) _cloudinaryManager.DeleteImageFromCloud(project.CoverImagePath);

            project = await _projectManager.UpdateAsync(
                project,
                input.GroupId,
                input.Title,
                input.Subtitle,
                input.TextInformation,
                input.CoverImagePath,
                input.Category,
                input.Status,
                input.RealeseDate,
                input.GoToUseLink,
                input.GoToGitLink
            );

            return ObjectMapper.Map<Project, ProjectDto>(project);
        }

        [Authorize(ProgGuruPermissions.Projects.Edit)]
        public async Task<bool> TryAdminUpdateAsync(Guid id, AdminUpdateProjectDto input)
        {
            var project = await _projectRepository.GetAsync(id);

            await _projectManager.AdminUpdateAsync(
                project,
                input.CreatorId,
                input.GroupId,
                input.Title,
                input.Subtitle,
                input.TextInformation,
                input.CoverImagePath,
                input.Category,
                input.Status,
                input.PublishDate,
                input.RealeseDate,
                input.GoToUseLink,
                input.GoToGitLink
            );

            return true;
        }

        [Authorize(ProgGuruPermissions.Projects.Edit)]
        public async Task<ProjectDto> AdminUpdateAsync(Guid id, AdminUpdateProjectDto input)
        {
            var project = await _projectRepository.GetAsync(id);

            if (input.CoverImagePath != project.CoverImagePath) _cloudinaryManager.DeleteImageFromCloud(project.CoverImagePath);

            project = await _projectManager.AdminUpdateAsync(
                project,
                input.CreatorId,
                input.GroupId,
                input.Title,
                input.Subtitle,
                input.TextInformation,
                input.CoverImagePath,
                input.Category,
                input.Status,
                input.PublishDate,
                input.RealeseDate,
                input.GoToUseLink,
                input.GoToGitLink
            );

            return ObjectMapper.Map<Project, ProjectDto>(project);
        }

        [Authorize(ProgGuruPermissions.Projects.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            var project = await _projectRepository.GetAsync(id);
            _cloudinaryManager.DeleteImageFromCloud(project.CoverImagePath);
            await _entityTagAdminAppService.RemoveEntityTagsAsync(new EntityTagRemoveDto { EntityId = id.ToString(), EntityType = "Project" });
            await _projectRepository.DeleteAsync(id);
        }

        public async Task<ListResultDto<GroupLookupDto>> GetGroupLookupAsync()
        {
            var groups = await _groupRepository.GetListAsync();

            return new ListResultDto<GroupLookupDto>(
                ObjectMapper.Map<List<Group>, List<GroupLookupDto>>(groups)
            );
        }

        public async Task<ListResultDto<UserLookupDto>> GetUserLookupAsync()
        {
            var users = await _userRepository.GetListAsync();

            return new ListResultDto<UserLookupDto>(
                ObjectMapper.Map<List<IdentityUser>, List<UserLookupDto>>(users)
            );
        }

        public virtual async Task SetEntityTagsAsync(EntityTagSetDto input)
        {
            await _entityTagAdminAppService.SetEntityTagsAsync(input);
        }

        public virtual async Task DeleteArticleAsync(Guid id)
        {
            await _articleAppService.DeleteAsync(id);
        }
    }
}
