using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Daridakr.ProgGuru.Entities.Users.Specializations
{
    public interface ISpecializationRepository : IRepository<Specialization, Guid>
    {
        Task<Specialization> FindByNameAsync(string name);

        Task<List<Specialization>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null
        );
    }
}
