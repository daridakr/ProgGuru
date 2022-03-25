using AutoMapper;
using Daridakr.ProgGuru;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Validation;
using Volo.CmsKit.Admin.Blogs;
using Volo.CmsKit.Blogs;

namespace Volo.CmsKit.Admin.Web.Pages.CmsKit.BlogPosts;

public class UpdateModel : CmsKitAdminPageModel
{
    [BindProperty]
    public virtual UpdateBlogPostViewModel ViewModel { get; set; }

    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public virtual Guid Id { get; set; }

    protected IBlogPostAdminAppService _blogPostAdminAppService { get; }

    protected IBlogFeatureAppService _blogFeatureAppService { get; }

    protected CoverImageAppService _coverImageAppService { get; }

    public IFormFile Cover { get; set; }

    public virtual BlogFeatureDto TagsFeature { get; protected set; }

    public UpdateModel(
        IBlogPostAdminAppService blogPostAdminAppService,
        IBlogFeatureAppService blogFeatureAppService,
        CoverImageAppService coverImageAppService)
    {
        _blogPostAdminAppService = blogPostAdminAppService;
        _blogFeatureAppService = blogFeatureAppService;
        _coverImageAppService = coverImageAppService;
    }

    public virtual async Task OnGetAsync()
    {
        var blogPost = await _blogPostAdminAppService.GetAsync(Id);

        ViewModel = ObjectMapper.Map<BlogPostDto, UpdateBlogPostViewModel>(blogPost);

        TagsFeature = await _blogFeatureAppService.GetOrDefaultAsync(blogPost.GroupId, GlobalFeatures.TagsFeature.Name);
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        var dto = ObjectMapper.Map<UpdateBlogPostViewModel, UpdateBlogPostDto>(ViewModel);

        try
        {
            if (await _blogPostAdminAppService.TryUpdateAsync(Id, dto))
            {
                if (Cover != null)
                {
                    var coverImageUri = await _coverImageAppService.UploadAsync(Cover);
                    dto.CoverImagePath = coverImageUri.ToString();
                }
                await _blogPostAdminAppService.UpdateAsync(Id, dto);
            }
        }
        catch (ArgumentException e)
        {
            throw new UserFriendlyException(e.Message, e.ParamName);
        }

        return NoContent();
    }

    [AutoMap(typeof(BlogPostDto))]
    [AutoMap(typeof(UpdateBlogPostDto), ReverseMap = true)]
    public class UpdateBlogPostViewModel : IHasConcurrencyStamp
    {
        [DynamicMaxLength(typeof(BlogPostConsts), nameof(BlogPostConsts.MaxTitleLength))]
        [Required]
        [DynamicFormIgnore]
        public string Title { get; set; }

        [DynamicStringLength(typeof(BlogPostConsts), nameof(BlogPostConsts.MaxSlugLength), nameof(BlogPostConsts.MinSlugLength))]
        [Required]
        [DisplayOrder(10000)]
        [DynamicFormIgnore]
        public string Slug { get; set; }

        [DynamicMaxLength(typeof(BlogPostConsts), nameof(BlogPostConsts.MaxShortDescriptionLength))]
        [DisplayOrder(10001)]
        public string ShortDescription { get; set; }

        [HiddenInput]
        [DynamicMaxLength(typeof(BlogPostConsts), nameof(BlogPostConsts.MaxContentLength))]
        public string Content { get; set; }

        [HiddenInput]
        [DataType(DataType.ImageUrl)]
        public string CoverImagePath { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}
