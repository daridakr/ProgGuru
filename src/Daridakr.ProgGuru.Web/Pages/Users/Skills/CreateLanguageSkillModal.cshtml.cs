using Daridakr.ProgGuru.Consts;
using Daridakr.ProgGuru.Enums.Users;
using Daridakr.ProgGuru.Users.Skills;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Daridakr.ProgGuru.Web.Pages.Users.Skills
{
    public class CreateLanguageSkillModalModel : ProgGuruPageModel
    {
        [BindProperty]
        public CreateLanguageSkillViewModel LanguageSkill { get; set; }

        private readonly ILanguageSkillAppService _languageSkillAppService;

        public CreateLanguageSkillModalModel(ILanguageSkillAppService languageSkillAppService)
        {
            _languageSkillAppService = languageSkillAppService;
        }

        public void OnGet()
        {
            LanguageSkill = new CreateLanguageSkillViewModel();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateLanguageSkillViewModel, CreateLanguageSkillDto>(LanguageSkill);

            await _languageSkillAppService.CreateAsync(dto);

            return NoContent();
        }

        public class CreateLanguageSkillViewModel
        {
            [Required]
            public Guid CreatorId { get; set; }

            [Required]
            [StringLength(UserConsts.MaxSkillNameLength)]
            public string Name { get; set; }

            [Required]
            public UserLanguageLevel LanguageLevel { get; set; } = UserLanguageLevel.Beginner;
        }
    }
}
