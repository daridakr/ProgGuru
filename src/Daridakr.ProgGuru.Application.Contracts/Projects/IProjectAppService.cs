using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Daridakr.ProgGuru.Projects
{
    public interface IProjectAppService : IApplicationService
    {
        Task<ProjectDto> GetAsync(Guid id);

        Task<PagedResultDto<ProjectDto>> GetListAsync(GetProjectListDto input);

        Task<bool> TryCreateAsync(CreateProjectDto input);

        Task<ProjectDto> CreateAsync(CreateProjectDto input);

        Task<bool> TryUpdateAsync(Guid id, UpdateProjectDto input);

        Task<ProjectDto> UpdateAsync(Guid id, UpdateProjectDto input);

        Task<bool> TryAdminUpdateAsync(Guid id, AdminUpdateProjectDto input);

        Task<ProjectDto> AdminUpdateAsync(Guid id, AdminUpdateProjectDto input);

        Task DeleteAsync(Guid id);

        Task<ListResultDto<GroupLookupDto>> GetGroupLookupAsync();

        Task<ListResultDto<UserLookupDto>> GetUserLookupAsync();
    }
}
