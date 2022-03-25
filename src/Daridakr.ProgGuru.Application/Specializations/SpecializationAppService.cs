using Daridakr.ProgGuru.Entities.Users.Specializations;
using Daridakr.ProgGuru.Users.Specializations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace Daridakr.ProgGuru.Specializations
{
    public class SpecializationAppService : ProgGuruAppService, ISpecializationAppService
    {
        private readonly ISpecializationRepository _specializationRepository;
        private readonly SpecializationManager _specializationManager;

        public SpecializationAppService(
            ISpecializationRepository specializationRepository,
            SpecializationManager specializationManager)
        {
            _specializationRepository = specializationRepository;
            _specializationManager = specializationManager;
        }

        public async Task<SpecializationDto> GetAsync(Guid id)
        {
            var specialization = await _specializationRepository.GetAsync(id);
            return ObjectMapper.Map<Specialization, SpecializationDto>(specialization);
        }

        public async Task<PagedResultDto<SpecializationDto>> GetListAsync(GetSpecializationListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Specialization.Name);
            }

            var specializations = await _specializationRepository.GetListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                input.Filter
            );

            var totalCount = input.Filter == null
                ? await _specializationRepository.CountAsync()
                : await _specializationRepository.CountAsync(
                    author => author.Name.Contains(input.Filter));

            return new PagedResultDto<SpecializationDto>(
                totalCount,
                ObjectMapper.Map<List<Specialization>, List<SpecializationDto>>(specializations)
            );
        }

        public async Task CreateAsync(SpecializationDto input)
        {
            var specialization = await _specializationManager.CreateAsync(
                input.CreatorId,
                input.Name
            );

            if (specialization != null) await _specializationRepository.InsertAsync(specialization);

            //return ObjectMapper.Map<Specialization, SpecializationDto>(specialization);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _specializationRepository.DeleteAsync(id);
        }

        public async Task<ListResultDto<SpecializationDto>> GetSpecializationLookupAsync()
        {
            var specializations = await _specializationRepository.GetListAsync();

            return new ListResultDto<SpecializationDto>(
                ObjectMapper.Map<List<Specialization>, List<SpecializationDto>>(specializations)
            );
        }
    }
}
