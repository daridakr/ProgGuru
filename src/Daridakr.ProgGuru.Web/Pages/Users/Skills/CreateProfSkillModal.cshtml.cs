using Daridakr.ProgGuru.Users.Skills;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Daridakr.ProgGuru.Web.Pages.Users.Skills
{
    public class CreateProfSkillModalModel : ProgGuruPageModel
    {
        [BindProperty]
        public CreateProfSkillViewModel ProfSkill { get; set; }

        public List<SelectListItem> Groups { get; set; }

        private readonly IProfSkillAppService _profSkillAppService;

        public CreateProfSkillModalModel(IProfSkillAppService profSkillAppService)
        {
            _profSkillAppService = profSkillAppService;
        }

        public async Task OnGetAsync()
        {
            ProfSkill = new CreateProfSkillViewModel();

            var groupLookup = await _profSkillAppService.GetGroupLookupAsync();
            Groups = groupLookup.Items
                .Select(x => new SelectListItem(x.Title, x.Id.ToString()))
                .ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateProfSkillViewModel, CreateProfSkillDto>(ProfSkill);

            await _profSkillAppService.CreateAsync(dto);

            return NoContent();
        }

        public class CreateProfSkillViewModel
        {
            [Required]
            public Guid CreatorId { get; set; }

            [Required]
            [SelectItems(nameof(Groups))]
            [DisplayName("Group")]
            public Guid GroupId { get; set; }

            [Required]
            public int BeginningYear { get; set; }

            public int EndYear { get; set; }

            [Required]
            public int KnownledgeLevel { get; set; }
        }
    }
}
