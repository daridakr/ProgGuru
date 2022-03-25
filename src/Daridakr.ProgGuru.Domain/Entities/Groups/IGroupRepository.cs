using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Daridakr.ProgGuru.Entities.Groups
{
    public interface IGroupRepository : IRepository<Group, Guid>
    {
        Task<Group> FindByIdAsync(Guid id);

        Task<Group> FindByTitleAsync(string title);

        Task<List<Group>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null
        );

    //    Task<long> GetCountAsync(
    //    string filter = null,
    //    Guid? roleId = null,
    //    Guid? organizationUnitId = null,
    //    string userName = null,
    //    string phoneNumber = null,
    //    string emailAddress = null,
    //    bool? isLockedOut = null,
    //    bool? notActive = null,
    //    CancellationToken cancellationToken = default
    //);
    }
}
