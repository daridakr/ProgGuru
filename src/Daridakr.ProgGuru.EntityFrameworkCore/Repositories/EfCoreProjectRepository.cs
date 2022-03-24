using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Daridakr.ProgGuru.Entities.Projects;
using Daridakr.ProgGuru.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Daridakr.ProgGuru.Repositories
{
    public class EfCoreProjectRepository
         : EfCoreRepository<ProgGuruDbContext, Project, Guid>,
            IProjectRepository
    {
        public EfCoreProjectRepository(
            IDbContextProvider<ProgGuruDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<Project> FindByGitHubLinksAmongUserProjectsAsync(string gitLink, Guid? userId)
        {
            var dbSet = await GetDbSetAsync();
            var userProjects = dbSet.Where(project => project.CreatorId == userId);
            return await userProjects.Where(project => project.GoToGitLink == gitLink).FirstOrDefaultAsync();
        }

        public async Task<Project> FindByGoToUseLinksAmongUserProjectsAsync(string projectLink, Guid? userId)
        {
            var dbSet = await GetDbSetAsync();
            var userProjects = dbSet.Where(project => project.CreatorId == userId);
            return await userProjects.Where(project => project.GoToUseLink == projectLink).FirstOrDefaultAsync();
        }

        public async Task<Project> FindByIdAsync(Guid projectId)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.FirstOrDefaultAsync(project => project.Id == projectId);
        }

        public async Task<List<Project>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    project => project.Title.Contains(filter)
                 )
                .OrderBy(sorting)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }

        public async  Task<List<Project>> GetOrderedList(Guid groupId, bool descending = false, CancellationToken cancellationToken = default)
        {
            if (!descending)
            {
                return await(await GetDbSetAsync()).Where(x => x.GroupId == groupId).OrderByDescending(x => x.CreationTime).ToListAsync(GetCancellationToken(cancellationToken));
            }
            else
            {
                return await(await GetDbSetAsync()).Where(x => x.GroupId == groupId).OrderBy(x => x.CreationTime).ToListAsync(GetCancellationToken(cancellationToken));
            }
        }

        public async Task<List<Project>> GetProjectsByGroupId(Guid id, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync()).Where(p => p.GroupId == id).OrderByDescending(p => p.CreationTime).ToListAsync(GetCancellationToken(cancellationToken));
        }

        public virtual async Task<int> GetCountAsync(
        string filter = null,
        CancellationToken cancellationToken = default)
        {
            var queryable = (await GetDbSetAsync())
               .WhereIf(!string.IsNullOrEmpty(filter), x => x.Title.Contains(filter));

            var count = await queryable.CountAsync(GetCancellationToken(cancellationToken));
            return count;
        }
    }
}
