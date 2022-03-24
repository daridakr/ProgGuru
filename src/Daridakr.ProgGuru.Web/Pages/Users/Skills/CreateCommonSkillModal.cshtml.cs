using Daridakr.ProgGuru.Consts;
using Daridakr.ProgGuru.Users.Skills;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Daridakr.ProgGuru.Web.Pages.Users.Skills
{
    public class CreateCommonSkillModalModel : ProgGuruPageModel
    {
        [BindProperty]
        public CreateCommonSkillViewModel CommonSkill { get; set; }

        private readonly ICommonSkillAppService _commonSkillAppService;

        public CreateCommonSkillModalModel(ICommonSkillAppService commonSkillAppService)
        {
            _commonSkillAppService = commonSkillAppService;
        }

        public void OnGet()
        {
            CommonSkill = new CreateCommonSkillViewModel();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateCommonSkillViewModel, CreateCommonSkillDto>(CommonSkill);
            await _commonSkillAppService.CreateAsync(dto);
            return NoContent();
        }

        public class CreateCommonSkillViewModel
        {
            [Required]
            public Guid CreatorId { get; set; }

            [Required]
            [StringLength(UserConsts.MaxSkillNameLength)]
            public string Name { get; set; }
        }
    }
}
