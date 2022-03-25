using Daridakr.ProgGuru.Consts;
using Daridakr.ProgGuru.Enums.Projects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Daridakr.ProgGuru.Localization;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using Daridakr.ProgGuru.Projects;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Volo.Abp;

namespace Daridakr.ProgGuru.Web.Pages.Projects.Admin
{
    public class EditModalModel : ProgGuruPageModel
    {
        [BindProperty]
        public AdminEditProjectViewModel Project { get; set; }

        public List<SelectListItem> Groups { get; set; }

        public List<SelectListItem> Users { get; set; }

        public IFormFile Cover { get; set; }

        private readonly IProjectAppService _projectAppService;

        private readonly CoverImageAppService _coverImageAppService;

        private readonly IStringLocalizer<ProgGuruResource> _localizer;

        public EditModalModel(
            IProjectAppService projectAppService,
            CoverImageAppService coverImageAppService,
            IStringLocalizer<ProgGuruResource> localizer)
        {
            _projectAppService = projectAppService;
            _coverImageAppService = coverImageAppService;
            _localizer = localizer;
        }

        public async Task OnGetAsync(Guid id)
        {
            var projectDto = await _projectAppService.GetAsync(id);
            Project = ObjectMapper.Map<ProjectDto, AdminEditProjectViewModel>(projectDto);

            var groupLookup = await _projectAppService.GetGroupLookupAsync();
            Groups = groupLookup.Items
                .Select(x => new SelectListItem(x.Title, x.Id.ToString()))
                .ToList();

            var userLookup = await _projectAppService.GetUserLookupAsync();
            Users = userLookup.Items
                .Where(x => x.UserName != "admin")
                .Select(x => new SelectListItem(x.UserName, x.Id.ToString()))
                .ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<AdminEditProjectViewModel, AdminUpdateProjectDto>(Project);
            ProjectDto updated = null;

            try
            {
                if (await _projectAppService.TryAdminUpdateAsync(Project.Id, dto))
                {
                    if (Cover != null)
                    {
                        var coverImageUri = await _coverImageAppService.UploadAsync(Cover);
                        dto.CoverImagePath = coverImageUri.ToString();
                    }
                    updated = await _projectAppService.AdminUpdateAsync(Project.Id, dto);
                }
            }
            catch (ArgumentException e)
            {
                throw new UserFriendlyException(_localizer[e.Message, e.ParamName]);
            }

            return new OkObjectResult(updated);
        }

        public class AdminEditProjectViewModel
        {
            [HiddenInput]
            public Guid Id { get; set; }

            [SelectItems(nameof(Groups))]
            [DisplayName("Group")]
            public Guid GroupId { get; set; }

            [SelectItems(nameof(Users))]
            [DisplayName("Creator")]
            public Guid CreatorId { get; set; }

            [Required]
            [StringLength(PostConsts.MaxTitleLength)]
            public string Title { get; set; }

            [Required]
            [StringLength(PostConsts.MaxSubtitleLength)]
            public string Subtitle { get; set; }

            [Required]
            [TextArea]
            [StringLength(PostConsts.MaxTextInformationLength)]
            public string TextInformation { get; set; }

            [Required]
            public ProjectCategory Category { get; set; } = ProjectCategory.Other;

            [Required]
            public ProjectStatus Status { get; set; } = ProjectStatus.During;

            [DataType(DataType.Date)]
            public DateTime PublishDate { get; set; }

            [DataType(DataType.Date)]
            public DateTime RealeseDate { get; set; }

            [DataType(DataType.Url)]
            public string GoToUseLink { get; set; }

            [DataType(DataType.Url)]
            public string GoToGitLink { get; set; }

            [Required]
            [DataType(DataType.ImageUrl)]
            public string CoverImagePath { get; set; }
        }
    }
}
