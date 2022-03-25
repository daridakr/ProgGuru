using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Daridakr.ProgGuru.Events
{
    public interface IEventAppService : IApplicationService
    {
        Task<EventDto> GetAsync(Guid id);

        //Task<PagedResultDto<EventDto>> GetListAsync(GetEventListDto input);

        Task<EventDto> CreateAsync(CreateEventDto input);

        Task UpdateAsync(Guid id, UpdateEventDto input);

        Task DeleteAsync(Guid id);
    }
}
