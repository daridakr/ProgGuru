using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.GlobalFeatures;
using Volo.CmsKit.GlobalFeatures;

namespace Volo.CmsKit.Public.Blogs;

[RequiresGlobalFeature(typeof(BlogsFeature))]
[RemoteService(Name = CmsKitPublicRemoteServiceConsts.RemoteServiceName)]
[Area(CmsKitPublicRemoteServiceConsts.ModuleName)]
[Route("api/cms-kit-public/blog-posts")]
public class BlogPostPublicController : CmsKitPublicControllerBase, IBlogPostPublicAppService
{
    protected IBlogPostPublicAppService BlogPostPublicAppService { get; }

    public BlogPostPublicController(IBlogPostPublicAppService blogPostPublicAppService)
    {
        BlogPostPublicAppService = blogPostPublicAppService;
    }

    [HttpGet]
    [Route("/{blogPostSlug}")]
    public virtual Task<BlogPostPublicDto> GetAsync(string blogPostSlug)
    {
        return BlogPostPublicAppService.GetAsync(blogPostSlug);
    }

    [HttpGet]
    [Route("{blogSlug}")]
    public virtual Task<PagedResultDto<BlogPostPublicDto>> GetListAsync(PagedAndSortedResultRequestDto input, string filter = null)
    {
        return BlogPostPublicAppService.GetListAsync(input, filter);
    }
}
