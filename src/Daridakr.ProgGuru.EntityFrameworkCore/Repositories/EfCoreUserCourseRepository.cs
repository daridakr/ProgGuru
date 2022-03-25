using Daridakr.ProgGuru.Entities.Users.Courses;
using Daridakr.ProgGuru.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Daridakr.ProgGuru.Repositories
{
    public class EfCoreUserCourseRepository
        : EfCoreRepository<ProgGuruDbContext, UserCourse, Guid>,
            IUserCourseRepository
    {
        public EfCoreUserCourseRepository(
            IDbContextProvider<ProgGuruDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<UserCourse> FindByIdAsync(Guid id)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.FirstOrDefaultAsync(cource => cource.Id == id);
        }

        public async Task<List<UserCourse>> GetListAsync(int skipCount, int maxResultCount, string sorting, string filter = null)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    course => course.Name.Contains(filter)
                 )
                .OrderBy(sorting)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }
    }
}
