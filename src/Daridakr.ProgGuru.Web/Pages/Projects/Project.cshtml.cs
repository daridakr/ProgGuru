using Daridakr.ProgGuru.Projects;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Daridakr.ProgGuru.Web.Pages.Projects
{
    public class ProjectModel : ProgGuruPageModel
    {
        public ProjectDto Project { get; set; }

        private readonly IProjectAppService _projectAppService;

        public ProjectModel(IProjectAppService projectAppService)
        {
            _projectAppService = projectAppService;
        }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            if (CurrentUser.IsAuthenticated) Project = await _projectAppService.GetAsync(id);
            else return Redirect("/account/login");
            return Page();
        }
    }
}
