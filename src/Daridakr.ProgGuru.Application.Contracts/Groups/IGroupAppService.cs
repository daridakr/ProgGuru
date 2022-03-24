using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Daridakr.ProgGuru.Groups
{
    public interface IGroupAppService : IApplicationService
    {
        Task<GroupDto> GetAsync(Guid id);

        Task<PagedResultDto<GroupDto>> GetListAsync(GetGroupListDto input);

        Task<bool> TryCreateAsync(CreateGroupDto input);

        Task<GroupDto> CreateAsync(CreateGroupDto input);

        Task<bool> TryUpdateAsync(Guid id, UpdateGroupDto input);

        Task<GroupDto> UpdateAsync(Guid id, UpdateGroupDto input);

        Task DeleteAsync(Guid id);
    }
}
