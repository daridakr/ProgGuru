using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using Daridakr.ProgGuru.Users.Skills;
using System.ComponentModel.DataAnnotations;
using System;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using System.Linq;
using System.ComponentModel;

namespace Daridakr.ProgGuru.Web.Pages.Users.Skills
{
    public class EditProfSkillModalModel : ProgGuruPageModel
    {
        [BindProperty]
        public EditProfSkillViewModel ProfSkill { get; set; }

        public List<SelectListItem> Groups { get; set; }

        private readonly IProfSkillAppService _profSkillAppService;

        public EditProfSkillModalModel(IProfSkillAppService profSkillAppService)
        {
            _profSkillAppService = profSkillAppService;
        }

        public async Task OnGetAsync(Guid id)
        {
            var profSkillDto = await _profSkillAppService.GetAsync(id);
            ProfSkill = ObjectMapper.Map<ProfSkillDto, EditProfSkillViewModel>(profSkillDto);

            var groupLookup = await _profSkillAppService.GetGroupLookupAsync();
            Groups = groupLookup.Items
                .Select(x => new SelectListItem(x.Title, x.Id.ToString()))
                .ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<EditProfSkillViewModel, UpdateProfSkillDto>(ProfSkill);

            await _profSkillAppService.UpdateAsync(ProfSkill.Id, dto);

            return NoContent();
        }

        public class EditProfSkillViewModel
        {
            [HiddenInput]
            public Guid Id { get; set; }

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
