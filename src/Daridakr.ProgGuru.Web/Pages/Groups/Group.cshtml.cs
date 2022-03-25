using Daridakr.ProgGuru.Groups;
using Daridakr.ProgGuru.Projects;
using Daridakr.ProgGuru.Subscriptions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Users;
using Volo.CmsKit.Admin.Blogs;

namespace Daridakr.ProgGuru.Web.Pages.Groups
{
    public class GroupModel : ProgGuruPageModel
    {
        public GroupDto Group { get; set; }

        public bool IsCurrentUserNotSubscribe { get; set; }

        public IReadOnlyList<ProjectDto> Projects { get; set; }

        public IReadOnlyList<BlogPostListDto> Articles { get; set; }

        public IReadOnlyList<GroupSubscriberDto> Subscribers { get; set; }

        private readonly IGroupAppService _groupAppService;

        private readonly IProjectAppService _projectAppService;

        private readonly IBlogPostAdminAppService _articleAppService;

        private readonly IGroupSubscriptionAppService _groupSubscriptionAppService;

        private readonly ICurrentUser _currentUser;

        public GroupModel(
            IGroupAppService groupAppService,
            IProjectAppService projectAppService,
            IBlogPostAdminAppService articleAppService,
            IGroupSubscriptionAppService groupSubscriptionAppService,
            ICurrentUser currentUser)
        {
            _groupAppService = groupAppService;
            _projectAppService = projectAppService;
            _articleAppService = articleAppService;
            _groupSubscriptionAppService = groupSubscriptionAppService;
            _currentUser = currentUser;
        }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            if (CurrentUser.IsAuthenticated)
            {
                Group = await _groupAppService.GetAsync(id);

                IsCurrentUserNotSubscribe = await _groupSubscriptionAppService.TrySubscribeAsync((Guid)_currentUser.Id, id);

                var articlesResult = await _articleAppService.GetListAsync(new BlogPostGetListInput { Filter = null, GroupId = id });
                Articles = articlesResult.Items.ToList();

                var projectsResult = await _projectAppService.GetListAsync(new GetProjectListDto { Filter = null });
                Projects = projectsResult.Items.Where(project => project.GroupId == id).ToList();

                var subscribersResult = await _groupSubscriptionAppService.GetGroupSubscribersAsync();
                Subscribers = subscribersResult.Where(subscriber => subscriber.GroupId == id).ToList();
            }
            else return Redirect("/account/login");
            return Page();
        }
    }
}
