using JetBrains.Annotations;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Daridakr.ProgGuru.Entities.Users.Specializations
{
    public class SpecializationManager : DomainService
    {
        private readonly ISpecializationRepository _specializationRepository;

        public SpecializationManager(ISpecializationRepository specializationRepository)
        {
            _specializationRepository = specializationRepository;
        }

        public async Task<Specialization> CreateAsync(
            [NotNull] Guid? creatorId,
            [NotNull] string name)
        {
            Check.NotNull(creatorId, nameof(creatorId));
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var existingSpecialization = await _specializationRepository.FindByNameAsync(name);
            //exist
            if (existingSpecialization != null)
            {
                return null;
            }

            return new Specialization(
                creatorId,
                name
            );
        }
    }
}
