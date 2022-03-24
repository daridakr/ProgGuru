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
using Daridakr.ProgGuru.Users.Courses;
using Microsoft.AspNetCore.Http;

namespace Daridakr.ProgGuru.Web.Pages.Users.Courses
{
    public class EditCourseModalModel : ProgGuruPageModel
    {
        [BindProperty]
        public EditCourceViewModel Course { get; set; }

        private readonly IUserCourseAppService _courseAppService;

        private readonly CoverImageAppService _coverImageAppService;

        private readonly IStringLocalizer<ProgGuruResource> _localizer;

        public IFormFile Cover { get; set; }

        public EditCourseModalModel(
            IUserCourseAppService courseAppService,
            IStringLocalizer<ProgGuruResource> localizer,
            CoverImageAppService coverImageAppService)
        {
            _courseAppService = courseAppService;
            _localizer = localizer;
            _coverImageAppService = coverImageAppService;
        }

        public async Task OnGetAsync(Guid id)
        {
            var courseDto = await _courseAppService.GetAsync(id);
            Course = ObjectMapper.Map<UserCourseDto, EditCourceViewModel>(courseDto);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<EditCourceViewModel, UpdateUserCourseDto>(Course);
            UserCourseDto updated = null;

            try
            {
                if (await _courseAppService.TryUpdateAsync(Course.Id, dto))
                {
                    if (Cover != null)
                    {
                        var coverImageUri = await _coverImageAppService.UploadAsync(Cover);
                        dto.CoverImagePath = coverImageUri.ToString();
                    }
                    updated = await _courseAppService.UpdateAsync(Course.Id, dto);
                }
            }
            catch (ArgumentException e)
            {
                throw new UserFriendlyException(_localizer[e.Message, e.ParamName]);
            }

            return NoContent();
        }

        public class EditCourceViewModel
        {
            [HiddenInput]
            public Guid Id { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [StringLength(UserConsts.MaxCourseLength)]
            public string Name { get; set; }

            [DataType(DataType.Text)]
            [StringLength(UserConsts.MaxCourseDescriptionLength)]
            public string Description { get; set; }

            [DataType(DataType.ImageUrl)]
            public string CoverImagePath { get; set; }
        }
    }
}
