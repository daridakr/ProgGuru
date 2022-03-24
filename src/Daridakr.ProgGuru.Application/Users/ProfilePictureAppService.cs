using Daridakr.ProgGuru.CloudStorage;
using Daridakr.ProgGuru.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace Daridakr.ProgGuru.Users
{
    [Authorize]
    public class ProfilePictureAppService : ProgGuruAppService
    {
        private readonly CloudinaryManager _cloudinaryManager;

        private readonly IRepository<IdentityUser, Guid> _userRepository;

        public ProfilePictureAppService(
            CloudinaryManager cloudinaryManager,
            IRepository<IdentityUser, Guid> repository)
        {
            _cloudinaryManager = cloudinaryManager;
            _userRepository = repository;
        }

        public virtual async Task UploadAsync([FromForm]IFormFile file)
        {
            if (file != null)
            {
                string imagePath;
                Uri uri;
                imagePath = await AppExtensions.GetFilePathAsync(file);
                uri = _cloudinaryManager.UploadImage(imagePath);
                var user = await _userRepository.GetAsync(CurrentUser.Id.Value).ConfigureAwait(false);
                var oldPicture = user.GetProperty<string>("ProfilePictureUrl");
                if (!oldPicture.Equals(UserConsts.DefaultProfilePicture))
                {
                    _cloudinaryManager.DeleteImageFromCloud(oldPicture);
                }
                user.SetProperty("ProfilePictureUrl", uri.ToString());
                await _userRepository.UpdateAsync(user).ConfigureAwait(false);
            }
        }
    }
}
