using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Daridakr.ProgGuru.Entities.Users.Courses
{
    public interface IUserCourseRepository : IRepository<UserCourse, Guid>
    {
        Task<UserCourse> FindByIdAsync(Guid id);

        Task<List<UserCourse>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null
        );
    }
}
