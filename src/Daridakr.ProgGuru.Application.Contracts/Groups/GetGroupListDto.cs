using Volo.Abp.Application.Dtos;

namespace Daridakr.ProgGuru.Groups
{
    // PagedAndSortedResultRequestDto has the standard paging and sorting properties: int MaxResultCount, int SkipCount and string Sorting
    public class GetGroupListDto : PagedAndSortedResultRequestDto
    {
        //  Is used to search authors. It can be null (or empty string) to get all the groups
        public string Filter { get; set; }
    }
}
