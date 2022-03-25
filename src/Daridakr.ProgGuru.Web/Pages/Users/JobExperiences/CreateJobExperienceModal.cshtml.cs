using Daridakr.ProgGuru.Users.JobExperiences;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Daridakr.ProgGuru.Users.Specializations;
using System.Linq;
using Volo.Abp;
using Microsoft.Extensions.Localization;
using Daridakr.ProgGuru.Localization;
using System;

namespace Daridakr.ProgGuru.Web.Pages.Users.JobExperiences
{
    public class CreateJobExperienceModalModel : ProgGuruPageModel
    {
        [BindProperty]
        public CreateJobExperienceDto JobExperience { get; set; }

        public List<SelectListItem> Specializations { get; set; }

        private readonly IJobExperienceAppService _jobExperienceAppService;

        private readonly ISpecializationAppService _specializationAppService;
        
        private readonly IStringLocalizer<ProgGuruResource> _localizer;

        public CreateJobExperienceModalModel(
            IJobExperienceAppService jobExperienceAppService,
            ISpecializationAppService specializationAppService,
            IStringLocalizer<ProgGuruResource> localizer)
        {
            _jobExperienceAppService = jobExperienceAppService;
            _specializationAppService = specializationAppService;
            _localizer = localizer;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            JobExperience = new CreateJobExperienceDto();
            var specializationsLookup = await _specializationAppService.GetSpecializationLookupAsync();
            Specializations = specializationsLookup.Items
                .Select(s => new SelectListItem(s.Name, s.Id.ToString()))
                .ToList();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                await _jobExperienceAppService.CreateAsync(JobExperience);
                await _specializationAppService.CreateAsync(new SpecializationDto { Name = JobExperience.Position, CreatorId = JobExperience.CreatorId });
            }
            catch (ArgumentException e)
            {
                throw new UserFriendlyException(_localizer[e.Message, e.ParamName]);
            }
            return NoContent();
        }
    }
}
