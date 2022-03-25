using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Volo.Abp;
using Microsoft.Extensions.Localization;
using Daridakr.ProgGuru.Localization;
using System;
using Microsoft.AspNetCore.Http;
using Daridakr.ProgGuru.Users.Courses;

namespace Daridakr.ProgGuru.Web.Pages.Users.Courses
{
    public class CreateCourseModalModel : ProgGuruPageModel
    {
        [BindProperty]
        public CreateUserCourseDto Course { get; set; }

        private readonly IUserCourseAppService _courseAppService;

        private readonly CoverImageAppService _coverImageAppService;

        private readonly IStringLocalizer<ProgGuruResource> _localizer;

        public IFormFile Cover { get; set; }

        public CreateCourseModalModel(
            IUserCourseAppService courseAppService,
            IStringLocalizer<ProgGuruResource> localizer,
            CoverImageAppService coverImageAppService)
        {
            _courseAppService = courseAppService;
            _localizer = localizer;
            _coverImageAppService = coverImageAppService;
        }

        public void OnGet()
        {
            Course = new CreateUserCourseDto();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Course.CoverImagePath = "";

            try
            {
                if (_courseAppService.TryCreateAsync(Course))
                {
                    if (Cover != null)
                    {
                        var coverImageUri = await _coverImageAppService.UploadAsync(Cover);
                        Course.CoverImagePath = coverImageUri.ToString();
                    }
                    await _courseAppService.CreateAsync(Course);
                }
            }
            catch (ArgumentException e)
            {
                throw new UserFriendlyException(e.Message, e.ParamName);
            }

            return NoContent();
        }
    }
}
