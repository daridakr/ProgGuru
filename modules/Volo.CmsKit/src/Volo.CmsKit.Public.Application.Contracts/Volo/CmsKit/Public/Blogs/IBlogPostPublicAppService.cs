using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Volo.CmsKit.Public.Blogs;

public interface IBlogPostPublicAppService : IApplicationService
{
    Task<PagedResultDto<BlogPostPublicDto>> GetListAsync(PagedAndSortedResultRequestDto input, string filter = null);

    Task<BlogPostPublicDto> GetAsync(string blogPostSlug);
}
