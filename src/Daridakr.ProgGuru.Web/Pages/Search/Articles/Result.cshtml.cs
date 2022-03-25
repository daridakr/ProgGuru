using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.CmsKit.Public.Blogs;

namespace Daridakr.ProgGuru.Web.Pages.Search.Articles
{
    public class ResultModel : ProgGuruPageModel
    {
        public IReadOnlyList<BlogPostPublicDto> Articles { get; set; }

        private readonly IBlogPostPublicAppService _articleAppService;

        public ResultModel(IBlogPostPublicAppService articleAppService)
        {
            _articleAppService = articleAppService;
        }

        public async Task<IActionResult> OnGetAsync(string searchString)
        {
            if (searchString != null)
            {
                if (CurrentUser.IsAuthenticated)
                {
                    var result = await _articleAppService.GetListAsync(new PagedAndSortedResultRequestDto(), searchString);
                    Articles = result.Items;
                }
                else return Redirect("/account/login");
            }
            return Page();
        }
    }
}
