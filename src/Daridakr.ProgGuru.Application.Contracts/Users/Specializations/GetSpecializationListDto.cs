using Volo.Abp.Application.Dtos;

namespace Daridakr.ProgGuru.Users.Specializations
{
    public class GetSpecializationListDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
