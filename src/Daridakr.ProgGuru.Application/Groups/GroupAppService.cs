using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Daridakr.ProgGuru.CloudStorage;
using Daridakr.ProgGuru.Entities.Groups;
using Daridakr.ProgGuru.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace Daridakr.ProgGuru.Groups
{
    [Authorize(ProgGuruPermissions.Groups.Default)]
    public class GroupAppService : ProgGuruAppService, IGroupAppService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly GroupManager _groupManager;
        private readonly CloudinaryManager _cloudinaryManager;

        public GroupAppService(
            IGroupRepository groupRepository,
            GroupManager groupManager,
            CloudinaryManager cloudinaryManager)
        {
            _groupRepository = groupRepository;
            _groupManager = groupManager;
            _cloudinaryManager = cloudinaryManager;
        }

        [Authorize(ProgGuruPermissions.Groups.View)]
        public async Task<GroupDto> GetAsync(Guid id)
        {
            var group = await _groupRepository.GetAsync(id);
            return ObjectMapper.Map<Group, GroupDto>(group);
        }

        [Authorize(ProgGuruPermissions.Groups.View)]
        public async Task<PagedResultDto<GroupDto>> GetListAsync(GetGroupListDto input)
        {
            // Default sorting is by group which is done in case of it wasn't sent by the client.
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Group.Title);
            }

            // get a paged, sorted and filtered list of groups from the database
            var groups = await _groupRepository.GetListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                input.Filter
            );

            // get the count of the groups
            var totalCount = input.Filter == null
                ? await _groupRepository.CountAsync()
                : await _groupRepository.CountAsync(
                    group => group.Title.Contains(input.Filter));

            // returning a paged result
            return new PagedResultDto<GroupDto>(
                totalCount,
                ObjectMapper.Map<List<Group>, List<GroupDto>>(groups)
            );
        }

        [Authorize(ProgGuruPermissions.Groups.Create)]
        public async Task<bool> TryCreateAsync(CreateGroupDto input)
        {
            await _groupManager.CreateAsync(
                input.CreatorId,
                input.Title,
                input.Subtitle,
                input.TextInformation,
                input.Developer,
                input.IssueYear,
                input.CoverImagePath,
                input.Website
            );

            return true;
        }

        [Authorize(ProgGuruPermissions.Groups.Create)]
        public async Task<GroupDto> CreateAsync(CreateGroupDto input)
        {
            var group = await _groupManager.CreateAsync(
                input.CreatorId,
                input.Title,
                input.Subtitle,
                input.TextInformation,
                input.Developer,
                input.IssueYear,
                input.CoverImagePath,
                input.Website
            );

            await _groupRepository.InsertAsync(group);

            // Return an GroupDto representing the newly created author
            return ObjectMapper.Map<Group, GroupDto>(group);
        }

        [Authorize(ProgGuruPermissions.Groups.Edit)]
        public async Task<bool> TryUpdateAsync(Guid id, UpdateGroupDto input)
        {
            var group = await _groupRepository.GetAsync(id);

            await _groupManager.UpdateAsync(
                group,
                input.Title,
                input.Subtitle,
                input.TextInformation,
                input.Developer,
                input.IssueYear,
                input.CoverImagePath,
                input.Website
            );

            return true;
        }

        [Authorize(ProgGuruPermissions.Groups.Edit)]
        public async Task<GroupDto> UpdateAsync(Guid id, UpdateGroupDto input)
        {
            var group = await _groupRepository.GetAsync(id);

            if(input.CoverImagePath != group.CoverImagePath) _cloudinaryManager.DeleteImageFromCloud(group.CoverImagePath);

            group = await _groupManager.UpdateAsync(
                group,
                input.Title,
                input.Subtitle,
                input.TextInformation,
                input.Developer,
                input.IssueYear,
                input.CoverImagePath,
                input.Website
            );

            //await _groupRepository.UpdateAsync(group);

            return ObjectMapper.Map<Group, GroupDto>(group);
        }

        [Authorize(ProgGuruPermissions.Groups.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            var group = await _groupRepository.GetAsync(id);
            _cloudinaryManager.DeleteImageFromCloud(group.CoverImagePath);
            await _groupRepository.DeleteAsync(id);
        }
    }
}
