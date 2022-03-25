using Daridakr.ProgGuru.Users.JobExperiences;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Daridakr.ProgGuru.Users.Specializations;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Localization;
using Daridakr.ProgGuru.Localization;
using System;
using Daridakr.ProgGuru.Consts;
using Daridakr.ProgGuru.Enums.Users;
using System.Linq;
using Volo.Abp;
using Volo.Abp.Users;

namespace Daridakr.ProgGuru.Web.Pages.Users.JobExperiences
{
    public class EditJobExperienceModalModel : ProgGuruPageModel
    {
        [BindProperty]
        public EditJobExperienceViewModel JobExperience { get; set; }

        public List<SelectListItem> Specializations { get; set; }

        private readonly IJobExperienceAppService _jobExperienceAppService;

        private readonly ISpecializationAppService _specializationAppService;

        private readonly IStringLocalizer<ProgGuruResource> _localizer;

        private readonly ICurrentUser _currentUser;

        public EditJobExperienceModalModel(
            IJobExperienceAppService jobExperienceAppService,
            ISpecializationAppService specializationAppService,
            IStringLocalizer<ProgGuruResource> localizer,
            ICurrentUser currentUser)
        {
            _jobExperienceAppService = jobExperienceAppService;
            _specializationAppService = specializationAppService;
            _localizer = localizer;
            _currentUser = currentUser;
        }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var jobExperienceDto = await _jobExperienceAppService.GetAsync(id);
            JobExperience = ObjectMapper.Map<JobExperienceDto, EditJobExperienceViewModel>(jobExperienceDto);

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
                var dto = ObjectMapper.Map<EditJobExperienceViewModel, UpdateJobExperienceDto>(JobExperience);

                await _jobExperienceAppService.UpdateAsync(JobExperience.Id, dto);
                await _specializationAppService.CreateAsync(new SpecializationDto { Name = JobExperience.Position, CreatorId = _currentUser.Id });
            }
            catch (ArgumentException e)
            {
                throw new UserFriendlyException(_localizer[e.Message, e.ParamName]);
            }
            return NoContent();
        }

        public class EditJobExperienceViewModel
        {
            [HiddenInput]
            public Guid Id { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [StringLength(UserConsts.MaxPositionLength)]
            public string Position { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [StringLength(UserConsts.MaxCompanyNameLength)]
            public string CompanyName { get; set; }

            public UserCategory PositionCategory { get; set; }

            [DataType(DataType.Text)]
            [StringLength(UserConsts.MaxLocationLength)]
            public string Location { get; set; }

            [Required]
            [DataType(DataType.Date)]
            public DateTime BeginningDate { get; set; }

            [DataType(DataType.Date)]
            public DateTime? EndDate { get; set; }
        }
    }
}
