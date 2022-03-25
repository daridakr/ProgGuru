using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Daridakr.ProgGuru.Consts;
using Daridakr.ProgGuru.Enums.Projects;
using Daridakr.ProgGuru.Localization;
using Daridakr.ProgGuru.Projects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Daridakr.ProgGuru.Web.Pages.Projects.Admin
{
    public class CreateModalModel : ProgGuruPageModel
    {
        [BindProperty]
        public AdminCreateProjectViewModel Project { get; set; }

        public List<SelectListItem> Groups { get; set; }

        public List<SelectListItem> Users { get; set; }

        public IFormFile Cover { get; set; }

        private readonly IStringLocalizer<ProgGuruResource> _localizer;

        private readonly IProjectAppService _projectAppService;

        private readonly CoverImageAppService _coverImageAppService;

        public CreateModalModel(
            IStringLocalizer<ProgGuruResource> localizer,
            IProjectAppService projectAppService,
            CoverImageAppService coverImageAppService)
        {
            _localizer = localizer;
            _projectAppService = projectAppService;
            _coverImageAppService = coverImageAppService;
        }

        public async Task OnGetAsync()
        {
            Project = new AdminCreateProjectViewModel();

            var groupLookup = await _projectAppService.GetGroupLookupAsync();
            Groups = groupLookup.Items
                .Select(x => new SelectListItem(x.Title, x.Id.ToString()))
                .ToList();

            var userLookup = await _projectAppService.GetUserLookupAsync();
            Users = userLookup.Items
                .Where(x => x.UserName!="admin")
                .Select(x => new SelectListItem(x.UserName, x.Id.ToString()))
                .ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Project.CoverImagePath = "dssdf";
            var dto = ObjectMapper.Map<AdminCreateProjectViewModel, CreateProjectDto>(Project);
            ProjectDto created = null;

            try
            {
                if(await _projectAppService.TryCreateAsync(dto))
                {
                    var coverImageUri = await _coverImageAppService.UploadAsync(Cover);
                    dto.CoverImagePath = coverImageUri.ToString();
                    created = await _projectAppService.CreateAsync(dto);
                }
            }
            catch(ArgumentException e)
            {
                throw new UserFriendlyException(_localizer[e.Message, e.ParamName]);
            }

            return new OkObjectResult(created);
        }

        public class AdminCreateProjectViewModel
        {
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
            [StringLength(PostConsts.MaxTextInformationLength)]
            [TextArea]
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
