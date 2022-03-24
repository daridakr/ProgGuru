using Daridakr.ProgGuru.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.Identity;

namespace Daridakr.ProgGuru.Users
{
    [Dependency(ReplaceServices = true)]
    [ExposeServices(typeof(IIdentityUserAppService), typeof(IdentityUserAppService), typeof(UserAppService))]
    public class UserAppService : IdentityUserAppService
    {
        public UserAppService(
        IdentityUserManager userManager,
        IIdentityUserRepository userRepository,
        IIdentityRoleRepository roleRepository,
        Microsoft.Extensions.Options.IOptions<Microsoft.AspNetCore.Identity.IdentityOptions> abpIdentityOptions)
            : base(userManager, userRepository, roleRepository, abpIdentityOptions)
        {
        }

        [Authorize(ProgGuruPermissions.Users.View)]
        public override async Task<IdentityUserDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<IdentityUser, IdentityUserDto>(
                await UserManager.GetByIdAsync(id)
            );
        }

        [Authorize(ProgGuruPermissions.Users.View)]
        public override async Task<PagedResultDto<IdentityUserDto>> GetListAsync(GetIdentityUsersInput input)
        {
            var count = await UserRepository.GetCountAsync(input.Filter);
            var list = await UserRepository.GetListAsync(input.Sorting, input.MaxResultCount, input.SkipCount, input.Filter);
            var users = list.Where(x => x.UserName != "admin").ToList();

            return new PagedResultDto<IdentityUserDto>(
                count,
                ObjectMapper.Map<List<IdentityUser>, List<IdentityUserDto>>(users)
            );
        }

        [Authorize(ProgGuruPermissions.Users.View)]
        public override async Task<IdentityUserDto> FindByUsernameAsync(string userName)
        {
            return ObjectMapper.Map<IdentityUser, IdentityUserDto>(
                await UserManager.FindByNameAsync(userName)
            );
        }

        [Authorize(ProgGuruPermissions.Users.View)]
        public override async Task<IdentityUserDto> FindByEmailAsync(string email)
        {
            return ObjectMapper.Map<IdentityUser, IdentityUserDto>(
            await UserManager.FindByEmailAsync(email)
            );
        }
    }
}
