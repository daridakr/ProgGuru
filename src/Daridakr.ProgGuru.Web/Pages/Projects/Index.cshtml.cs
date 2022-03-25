using Daridakr.ProgGuru.Projects;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Daridakr.ProgGuru.Web.Pages.Projects
{
    public class UserIndexModel : ProgGuruPageModel
    {
        public IReadOnlyList<ProjectDto> Projects { get; set; }

        private readonly IProjectAppService _projectAppService;

        public UserIndexModel(IProjectAppService projectAppService)
        {
            _projectAppService = projectAppService;
        }

        public async Task OnGetAsync()
        {
            var result = await _projectAppService.GetListAsync(new GetProjectListDto { Filter = null });
            Projects = result.Items.Where(project => project.CreatorId == CurrentUser.Id).ToList();
        }
    }
}
