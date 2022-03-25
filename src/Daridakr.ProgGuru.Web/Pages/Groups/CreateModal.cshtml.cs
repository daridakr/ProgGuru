using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Daridakr.ProgGuru.Consts;
using Daridakr.ProgGuru.Groups;
using Daridakr.ProgGuru.Localization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Daridakr.ProgGuru.Web.Pages.Groups
{
    public class CreateModalModel : ProgGuruPageModel
    {
        [BindProperty]
        public CreateGroupViewModel Group { get; set; }

        public IFormFile Cover { get; set; }

        private readonly IGroupAppService _groupAppService;

        private readonly CoverImageAppService _coverImageAppService;

        private readonly IStringLocalizer<ProgGuruResource> _localizer;

        public CreateModalModel(
            IGroupAppService groupAppService,
            CoverImageAppService coverImageAppService,
            IStringLocalizer<ProgGuruResource> localizer)
        {
            _groupAppService = groupAppService;
            _coverImageAppService = coverImageAppService;
            _localizer = localizer;
        }

        public void OnGet()
        {
            Group = new CreateGroupViewModel();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Group.CoverImagePath = "dssdf";
            var dto = ObjectMapper.Map<CreateGroupViewModel, CreateGroupDto>(Group);

            try
            {
                if (await _groupAppService.TryCreateAsync(dto))
                {
                    var coverImageUri = await _coverImageAppService.UploadAsync(Cover);
                    dto.CoverImagePath = coverImageUri.ToString();
                    await _groupAppService.CreateAsync(dto);
                }
            }
            catch (ArgumentException e)
            {
                throw new UserFriendlyException(_localizer[e.Message, e.ParamName]);
            }
            return NoContent();
        }

        public class CreateGroupViewModel
        {
            [Required]
            public Guid CreatorId { get; set; }

            [Required]
            [StringLength(GroupConsts.MaxTitleLength)]
            public string Title { get; set; }

            [Required]
            [StringLength(GroupConsts.MaxSubtitleLength)]
            public string Subtitle { get; set; }

            [Required]
            [TextArea]
            [StringLength(GroupConsts.MaxTextInformationLength)]
            public string TextInformation { get; set; }

            [Required]
            [StringLength(GroupConsts.MaxDeveloperLength)]
            public string Developer { get; set; }

            [Required]
            public int IssueYear { get; set; }

            [StringLength(GroupConsts.MaxWebsiteLength)]
            public string Website { get; set; }

            [Required]
            [DataType(DataType.ImageUrl)]
            public string CoverImagePath { get; set; }
        }
    }
}