using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.CmsKit.Public.Blogs;

namespace Daridakr.ProgGuru.Web.Pages.Articles
{
    public class AllModel : ProgGuruPageModel
    {
        public IReadOnlyList<BlogPostPublicDto> Articles { get; set; }

        private readonly IBlogPostPublicAppService _articleAppService;

        public AllModel(IBlogPostPublicAppService articleAppService)
        {
            _articleAppService = articleAppService;
        }

        public async Task OnGetAsync()
        {
            var result = await _articleAppService.GetListAsync(new PagedAndSortedResultRequestDto());
            Articles = result.Items;
        }
    }
}
