using Daridakr.ProgGuru.Projects;
using Daridakr.ProgGuru.Subscriptions;
using Daridakr.ProgGuru.Users.Courses;
using Daridakr.ProgGuru.Users.JobExperiences;
using Daridakr.ProgGuru.Users.Skills;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Identity;
using Volo.Abp.Users;
using Volo.CmsKit.Admin.Blogs;
using Volo.CmsKit.Public.Comments;

namespace Daridakr.ProgGuru.Web.Pages.Users
{
    public class UserModel : ProgGuruPageModel
    {
        public new IdentityUserDto User { get; set; }

        public bool IsCurrentUserNotSubscribe { get; set; }

        public int CommentCount { get; set; }

        public IReadOnlyList<CommonSkillDto> CommonSkills { get; set; }

        public IReadOnlyList<LanguageSkillDto> LanguageSkills { get; set; }

        public IReadOnlyList<ProfSkillDto> ProfSkills { get; set; }

        public IReadOnlyList<JobExperienceDto> JobExperiences { get; set; }

        public IReadOnlyList<UserCourseDto> Courses { get; set; }

        public IReadOnlyList<ProjectDto> Projects { get; set; }

        public IReadOnlyList<BlogPostListDto> Articles { get; set; }

        public IReadOnlyList<UserGroupSubscriptionDto> GroupSubscriptions { get; set; }

        public IReadOnlyList<UserSubscriptionDto> UserSubscriptions { get; set; }

        public IReadOnlyList<UserSubscriptionDto> Subscribers { get; set; }

        private readonly IIdentityUserAppService _identityUserAppService;

        private readonly ICommonSkillAppService _commonSkillAppService;

        private readonly ILanguageSkillAppService _languageSkillAppService;

        private readonly IProfSkillAppService _profSkillAppService;

        private readonly IJobExperienceAppService _jobExperienceAppService;

        private readonly IUserCourseAppService _courseAppService;

        private readonly IProjectAppService _projectAppService;

        private readonly IBlogPostAdminAppService _articleAppService;

        private readonly ICommentPublicAppService _commentAppService;

        private readonly IGroupSubscriptionAppService _groupSubscriptionAppService;

        private readonly IUserSubscriptionAppService _userSubscriptionAppService;

        private readonly ICurrentUser _currentUser;

        public UserModel(
            IIdentityUserAppService identityUserAppService,
            ICommonSkillAppService commonSkillAppService,
            ILanguageSkillAppService languageSkillAppService,
            IProfSkillAppService profSkillAppService,
            IJobExperienceAppService jobExperienceAppService,
            IUserCourseAppService courseAppService,
            IProjectAppService projectAppService,
            IBlogPostAdminAppService articleAppService,
            ICommentPublicAppService commentAppService,
            IGroupSubscriptionAppService groupSubscriptionAppService,
            IUserSubscriptionAppService userSubscriptionAppService,
            ICurrentUser currentUser)
        {
            _identityUserAppService = identityUserAppService;
            _commonSkillAppService = commonSkillAppService;
            _languageSkillAppService = languageSkillAppService;
            _profSkillAppService = profSkillAppService;
            _jobExperienceAppService = jobExperienceAppService;
            _courseAppService = courseAppService;
            _projectAppService = projectAppService;
            _articleAppService = articleAppService;
            _commentAppService = commentAppService;
            _groupSubscriptionAppService = groupSubscriptionAppService;
            _userSubscriptionAppService = userSubscriptionAppService;
            _currentUser = currentUser;
        }

        public async Task<IActionResult> OnGetAsync(string userName)
        {
            if(CurrentUser.IsAuthenticated)
            {
                User = await _identityUserAppService.FindByUsernameAsync(userName);

                if (User.UserName == "admin") return Redirect("/");
                else
                {
                    var commonSkillsResult = await _commonSkillAppService.GetListAsync();
                    CommonSkills = commonSkillsResult.Where(commonSkill => commonSkill.CreatorId == User.Id).ToList();

                    var languageSkillsResult = await _languageSkillAppService.GetListAsync();
                    LanguageSkills = languageSkillsResult.Where(languageSkill => languageSkill.CreatorId == User.Id).ToList();

                    var profSkillsResult = await _profSkillAppService.GetListAsync();
                    ProfSkills = profSkillsResult.Where(profSkill => profSkill.CreatorId == User.Id).ToList();

                    var jobExperiencesResult = await _jobExperienceAppService.GetListAsync();
                    JobExperiences = jobExperiencesResult.Where(jobExperience => jobExperience.CreatorId == User.Id).ToList();

                    var coursesResult = await _courseAppService.GetListAsync();
                    Courses = coursesResult.Where(course => course.CreatorId == User.Id).ToList();

                    var projectsResult = await _projectAppService.GetListAsync(new GetProjectListDto { Filter = null });
                    Projects = projectsResult.Items.Where(project => project.CreatorId == User.Id).ToList();

                    var articlesResult = await _articleAppService.GetListAsync(new BlogPostGetListInput());
                    Articles = articlesResult.Items.Where(article => article.CreatorId == User.Id).ToList();

                    var usersComments = await _commentAppService.GetListAsync("User", User.Id.ToString());
                    CommentCount = usersComments.Items.Count();

                    var groupSubscriptionsResult = await _groupSubscriptionAppService.GetUserSubscriptionsAsync();
                    GroupSubscriptions = groupSubscriptionsResult.Where(groupSubscription => groupSubscription.CreatorId == User.Id).ToList();

                    IsCurrentUserNotSubscribe = await _userSubscriptionAppService.TrySubscribeAsync((Guid)_currentUser.Id, User.Id);

                    var userSubscriptionsResult = await _userSubscriptionAppService.GetUserSubscriptionsAsync();
                    UserSubscriptions = userSubscriptionsResult.Where(userSubscription => userSubscription.CreatorId == User.Id).ToList();

                    var userSubscribersResult = await _userSubscriptionAppService.GetUserSubscribersAsync();
                    Subscribers = userSubscribersResult.Where(userSubscriber => userSubscriber.UserId == User.Id).ToList();

                    return Page();
                } 
            }
            else return Redirect("/account/login");
        }
    }
}
