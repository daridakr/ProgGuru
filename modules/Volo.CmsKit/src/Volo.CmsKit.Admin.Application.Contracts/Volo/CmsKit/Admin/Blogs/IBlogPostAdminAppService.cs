using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Volo.CmsKit.Admin.Blogs;

public interface IBlogPostAdminAppService
    : ICrudAppService<
        BlogPostDto,
        BlogPostListDto,
        Guid,
        BlogPostGetListInput,
        CreateBlogPostDto,
        UpdateBlogPostDto>
{
    Task<bool> TryCreateAsync(CreateBlogPostDto input);

    Task<bool> TryUpdateAsync(Guid id, UpdateBlogPostDto input);
}
