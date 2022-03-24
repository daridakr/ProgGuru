using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Daridakr.ProgGuru.Entities.Projects
{
    public interface IProjectRepository : IRepository<Project, Guid>
    {
        Task<List<Project>> GetProjectsByGroupId(Guid id, CancellationToken cancellationToken = default);

        Task<Project> FindByIdAsync(Guid projectId);

        Task<Project> FindByGitHubLinksAmongUserProjectsAsync(string gitLink, Guid? userId);

        Task<Project> FindByGoToUseLinksAmongUserProjectsAsync(string projectLink, Guid? userId);

        Task<List<Project>> GetOrderedList(Guid groupId, bool descending = false, CancellationToken cancellationToken = default);

        Task<List<Project>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null
        );

        Task<int> GetCountAsync(
        string filter = null,
        CancellationToken cancellationToken = default);
    }
}
