using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Daridakr.ProgGuru.Users.Specializations
{
    public interface ISpecializationAppService : IApplicationService
    {
        Task<SpecializationDto> GetAsync(Guid id);

        Task<PagedResultDto<SpecializationDto>> GetListAsync(GetSpecializationListDto input);

        Task CreateAsync(SpecializationDto input);

        Task DeleteAsync(Guid id);

        Task<ListResultDto<SpecializationDto>> GetSpecializationLookupAsync();
    }
}
