using AutoMapper;
using Daridakr.ProgGuru;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using Volo.Abp.Validation;
using Volo.CmsKit.Admin.Blogs;
using Volo.CmsKit.Blogs;

namespace Volo.CmsKit.Admin.Web.Pages.CmsKit.BlogPosts;

public class CreateModel : CmsKitAdminPageModel
{
    [BindProperty]
    public CreateBlogPostViewModel ViewModel { get; set; }

    protected IBlogPostAdminAppService _blogPostAdminAppService { get; }

    protected CoverImageAppService _coverImageAppService { get; }

    public IFormFile Cover { get; set; }

    public CreateModel(
        IBlogPostAdminAppService blogPostAdminAppService,
        CoverImageAppService coverImageAppService)
    {
        _blogPostAdminAppService = blogPostAdminAppService;
        _coverImageAppService = coverImageAppService;
    }

    public void OnGet()
    {
        ViewModel = new CreateBlogPostViewModel();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        ViewModel.CoverImagePath = "";
        var dto = ObjectMapper.Map<CreateBlogPostViewModel, CreateBlogPostDto>(ViewModel);
        BlogPostDto created = null;

        try
        {
            if (await _blogPostAdminAppService.TryCreateAsync(dto))
            {
                if (Cover != null)
                {
                    var coverImageUri = await _coverImageAppService.UploadAsync(Cover);
                    dto.CoverImagePath = coverImageUri.ToString();
                }
                created = await _blogPostAdminAppService.CreateAsync(dto);
            }
        }
        catch (ArgumentException e)
        {
            throw new UserFriendlyException(e.Message, e.ParamName);
        }

        return new OkObjectResult(created);
    }

    [AutoMap(typeof(CreateBlogPostDto), ReverseMap = true)]
    public class CreateBlogPostViewModel
    {
        [Required]
        [DynamicFormIgnore]
        public Guid CreatorId { get; set; }

        [Required]
        [DynamicFormIgnore]
        public Guid GroupId { get; set; }

        [Required]
        [DynamicMaxLength(typeof(BlogPostConsts), nameof(BlogPostConsts.MaxTitleLength))]
        [DynamicFormIgnore]
        public string Title { get; set; }

        [Required]
        [DynamicStringLength(
            typeof(BlogPostConsts),
            nameof(BlogPostConsts.MaxSlugLength),
            nameof(BlogPostConsts.MinSlugLength))]
        [DynamicFormIgnore]
        public string Slug { get; set; }

        [DynamicMaxLength(typeof(BlogPostConsts), nameof(BlogPostConsts.MaxShortDescriptionLength))]
        public string ShortDescription { get; set; }

        [HiddenInput]
        [DynamicMaxLength(typeof(BlogPostConsts), nameof(BlogPostConsts.MaxContentLength))]
        public string Content { get; set; }

        [HiddenInput]

        [DataType(DataType.ImageUrl)]
        public string CoverImagePath { get; set; }
    }
}
