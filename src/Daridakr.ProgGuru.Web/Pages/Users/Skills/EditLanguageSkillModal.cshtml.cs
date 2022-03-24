using Daridakr.ProgGuru.Consts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Daridakr.ProgGuru.Enums.Users;
using Daridakr.ProgGuru.Users.Skills;
using System.ComponentModel.DataAnnotations;
using System;

namespace Daridakr.ProgGuru.Web.Pages.Users.Skills
{
    public class EditLanguageSkillModalModel : ProgGuruPageModel
    {
        [BindProperty]
        public EditLanguageSkillViewModel LanguageSkill { get; set; }

        private readonly ILanguageSkillAppService _languageSkillAppService;

        public EditLanguageSkillModalModel(ILanguageSkillAppService languageSkillAppService)
        {
            _languageSkillAppService = languageSkillAppService;
        }

        public async Task OnGetAsync(Guid id)
        {
            var languageSkillDto = await _languageSkillAppService.GetAsync(id);
            LanguageSkill = ObjectMapper.Map<LanguageSkillDto, EditLanguageSkillViewModel>(languageSkillDto);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<EditLanguageSkillViewModel, UpdateLanguageSkillDto>(LanguageSkill);

            await _languageSkillAppService.UpdateAsync(LanguageSkill.Id, dto);

            return NoContent();
        }

        public class EditLanguageSkillViewModel
        {
            [HiddenInput]
            public Guid Id { get; set; }

            [Required]
            [StringLength(UserConsts.MaxSkillNameLength)]
            public string Name { get; set; }

            [Required]
            public UserLanguageLevel LanguageLevel { get; set; } = UserLanguageLevel.Beginner;
        }
    }
}
