using Daridakr.ProgGuru.Projects;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Daridakr.ProgGuru.Web.Pages.Search.Projects
{
    public class ResultModel : ProgGuruPageModel
    {
        public IReadOnlyList<ProjectDto> Projects { get; set; }

        private readonly IProjectAppService _projectAppService;

        public ResultModel(IProjectAppService projectAppService)
        {
            _projectAppService = projectAppService;
        }

        public async Task<IActionResult> OnGetAsync(string searchString)
        {
            if (searchString != null)
            {
                if (CurrentUser.IsAuthenticated)
                {
                    var result = await _projectAppService.GetListAsync(new GetProjectListDto { Filter = searchString });
                    Projects = result.Items;
                }
                else return Redirect("/account/login");
            }
            return Page();
        }
    }
}
