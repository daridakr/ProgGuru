using Volo.Abp.Application.Dtos;

namespace Daridakr.ProgGuru.Projects
{
    public class GetProjectListDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
