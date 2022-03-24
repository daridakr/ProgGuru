using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Daridakr.ProgGuru.Users.Courses
{
    public interface IUserCourseAppService : IApplicationService
    {
        Task<UserCourseDto> GetAsync(Guid id);

        Task<List<UserCourseDto>> GetListAsync();

        bool TryCreateAsync(CreateUserCourseDto input);

        Task<UserCourseDto> CreateAsync(CreateUserCourseDto input);

        Task<bool> TryUpdateAsync(Guid id, UpdateUserCourseDto input);

        Task<UserCourseDto> UpdateAsync(Guid id, UpdateUserCourseDto input);

        Task DeleteAsync(Guid id);
    }
}
