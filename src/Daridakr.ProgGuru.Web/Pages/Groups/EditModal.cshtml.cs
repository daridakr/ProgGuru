using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Daridakr.ProgGuru.Consts;
using Daridakr.ProgGuru.Groups;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Daridakr.ProgGuru.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using Volo.Abp;

namespace Daridakr.ProgGuru.Web.Pages.Groups
{
    public class EditModalModel : ProgGuruPageModel
    {
        [BindProperty]
        public EditGroupViewModel Group { get; set; }

        public IFormFile Cover { get; set; }

        private readonly IGroupAppService _groupAppService;

        private readonly CoverImageAppService _coverImageAppService;

        private readonly IStringLocalizer<ProgGuruResource> _localizer;

        public EditModalModel(
            IGroupAppService groupAppService,
            CoverImageAppService coverImageAppService,
            IStringLocalizer<ProgGuruResource> localizer)
        {
            _groupAppService = groupAppService;
            _coverImageAppService = coverImageAppService;
            _localizer = localizer;
        }

        public async Task OnGetAsync(Guid id)
        {
            var groupDto = await _groupAppService.GetAsync(id);
            Group = ObjectMapper.Map<GroupDto, EditGroupViewModel>(groupDto);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<EditGroupViewModel, UpdateGroupDto>(Group);

            try
            {
                if (await _groupAppService.TryUpdateAsync(Group.Id, dto))
                {
                    if (Cover != null)
                    {
                        var coverImageUri = await _coverImageAppService.UploadAsync(Cover);
                        dto.CoverImagePath = coverImageUri.ToString();
                    }
                    await _groupAppService.UpdateAsync(Group.Id, dto);
                }
            }
            catch (ArgumentException e)
            {
                throw new UserFriendlyException(_localizer[e.Message, e.ParamName]);
            }

            return NoContent();
        }

        public class EditGroupViewModel
        {
            [HiddenInput]
            public Guid Id { get; set; }

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
