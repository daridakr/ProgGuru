using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Data;
using Volo.Abp.GlobalFeatures;
using Volo.CmsKit.Blogs;
using Volo.CmsKit.GlobalFeatures;
using Volo.CmsKit.Permissions;
using Volo.CmsKit.Users;
using Daridakr.ProgGuru.Entities.Groups;
using Volo.Abp.Identity;
using Daridakr.ProgGuru.CloudStorage;
using Volo.CmsKit.Admin.Tags;

namespace Volo.CmsKit.Admin.Blogs;

[RequiresGlobalFeature(typeof(BlogsFeature))]
[Authorize(CmsKitAdminPermissions.BlogPosts.Default)]
public class BlogPostAdminAppService : CmsKitAppServiceBase, IBlogPostAdminAppService
{
    protected IBlogPostRepository BlogPostRepository { get; }

    protected IGroupRepository GroupRepository { get; }

    protected IIdentityUserRepository UserRepository { get; }

    protected ICmsUserLookupService UserLookupService { get; }

    protected BlogPostManager BlogPostManager { get; }

    protected CloudinaryManager _cloudinaryManager { get; }

    protected EntityTagAdminAppService _entityTagAdminAppService { get; }

    public BlogPostAdminAppService(
        BlogPostManager blogPostManager,
        CloudinaryManager cloudinaryManager,
        IBlogPostRepository blogPostRepository,
        IGroupRepository groupRepository,
        ICmsUserLookupService userLookupService,
        IIdentityUserRepository userRepository,
        EntityTagAdminAppService entityTagAdminAppService)
    {
        BlogPostManager = blogPostManager;
        _cloudinaryManager = cloudinaryManager;
        BlogPostRepository = blogPostRepository;
        GroupRepository = groupRepository;
        UserRepository = userRepository;
        UserLookupService = userLookupService;
        _entityTagAdminAppService = entityTagAdminAppService;
    }

    [Authorize(CmsKitAdminPermissions.BlogPosts.Default)]
    public virtual async Task<BlogPostDto> GetAsync(Guid id)
    {
        var blogPost = await BlogPostRepository.GetAsync(id);

        var blogPostDto = ObjectMapper.Map<BlogPost, BlogPostDto>(blogPost);

        return blogPostDto;
    }

    [Authorize(CmsKitAdminPermissions.BlogPosts.Default)]
    public virtual async Task<PagedResultDto<BlogPostListDto>> GetListAsync(BlogPostGetListInput input)
    {
        var groups = (await GroupRepository.GetListAsync()).ToDictionary(x => x.Id);

        var users = (await UserRepository.GetListAsync()).ToDictionary(x => x.Id);

        var blogPosts = await BlogPostRepository.GetListAsync(input.Filter, input.GroupId, input.MaxResultCount, input.SkipCount, input.Sorting);

        var count = await BlogPostRepository.GetCountAsync(input.Filter);

        var dtoList = blogPosts.Select(x =>
        {
            var dto = ObjectMapper.Map<BlogPost, BlogPostListDto>(x);
            dto.GroupTitle = groups[x.GroupId].Title;
            dto.CreatorUserName = users[(Guid)x.CreatorId].UserName;

            return dto;
        }).ToList();

        return new PagedResultDto<BlogPostListDto>(count, dtoList);
    }

    [Authorize(CmsKitAdminPermissions.BlogPosts.Create)]
    public async Task<bool> TryCreateAsync(CreateBlogPostDto input)
    {
        await BlogPostManager.CreateAsync(
            input.CreatorId,
            input.GroupId,
            input.Title,
            input.Slug,
            input.ShortDescription,
            input.Content,
            input.CoverImagePath
        );

        return true;
    }

    [Authorize(CmsKitAdminPermissions.BlogPosts.Create)]
    public virtual async Task<BlogPostDto> CreateAsync(CreateBlogPostDto input)
    {
        var blogPost = await BlogPostManager.CreateAsync(
                                                    input.CreatorId,
                                                    input.GroupId,
                                                    input.Title,
                                                    input.Slug,
                                                    input.ShortDescription,
                                                    input.Content,
                                                    input.CoverImagePath);

        await BlogPostRepository.InsertAsync(blogPost);

        return ObjectMapper.Map<BlogPost, BlogPostDto>(blogPost);
    }

    [Authorize(CmsKitAdminPermissions.BlogPosts.Update)]
    public async Task<bool> TryUpdateAsync(Guid id, UpdateBlogPostDto input)
    {
        var blogPost = await BlogPostRepository.GetAsync(id);
        blogPost.SetTitle(input.Title);
        blogPost.SetShortDescription(input.ShortDescription);
        blogPost.SetContent(input.Content);
        blogPost.SetConcurrencyStampIfNotNull(input.ConcurrencyStamp);
        if (blogPost.Slug != input.Slug)
        {
            await BlogPostManager.SetSlugUrlAsync(blogPost, input.Slug);
        }

        return true;
    }

    [Authorize(CmsKitAdminPermissions.BlogPosts.Update)]
    public virtual async Task<BlogPostDto> UpdateAsync(Guid id, UpdateBlogPostDto input)
    {
        var blogPost = await BlogPostRepository.GetAsync(id);

        if (input.CoverImagePath != blogPost.CoverImagePath)
        {
            if (blogPost.CoverImagePath != "") _cloudinaryManager.DeleteImageFromCloud(blogPost.CoverImagePath);
        }
        
        blogPost.SetTitle(input.Title);
        blogPost.SetShortDescription(input.ShortDescription);
        blogPost.SetContent(input.Content);
        blogPost.SetConcurrencyStampIfNotNull(input.ConcurrencyStamp);
        blogPost.CoverImagePath = input.CoverImagePath;

        if (blogPost.Slug != input.Slug)
        {
            await BlogPostManager.SetSlugUrlAsync(blogPost, input.Slug);
        }

        await BlogPostRepository.UpdateAsync(blogPost);

        return ObjectMapper.Map<BlogPost, BlogPostDto>(blogPost);
    }

    [Authorize(CmsKitAdminPermissions.BlogPosts.Delete)]
    public virtual async Task DeleteAsync(Guid id)
    {
        var blogPost = await BlogPostRepository.GetAsync(id);
        if (blogPost.CoverImagePath != "") _cloudinaryManager.DeleteImageFromCloud(blogPost.CoverImagePath);
        await _entityTagAdminAppService.RemoveEntityTagsAsync(new EntityTagRemoveDto { EntityId = id.ToString(), EntityType = "BlogPost" });
        await BlogPostRepository.DeleteAsync(id);
    }
}
