using Daridakr.ProgGuru.Projects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Daridakr.ProgGuru.Web.Pages.Projects
{
    public class AdminIndexModel : ProgGuruPageModel
    {
        public IReadOnlyList<ProjectDto> Projects { get; set; }

        private readonly IProjectAppService _projectAppService;

        public AdminIndexModel(IProjectAppService projectAppService)
        {
            _projectAppService = projectAppService;
        }

        public async Task OnGetAsync()
        {
            var result = await _projectAppService.GetListAsync(new GetProjectListDto { Filter = null });
            Projects = result.Items;
        }
    }
}
