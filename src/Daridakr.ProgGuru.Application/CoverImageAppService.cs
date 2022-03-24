using Daridakr.ProgGuru.CloudStorage;
using Daridakr.ProgGuru.Entities.Projects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Daridakr.ProgGuru
{
    [Authorize]
    public class CoverImageAppService : ProgGuruAppService
    {
        private readonly CloudinaryManager _cloudinaryManager;

        private readonly IProjectRepository _projectRepository;

        public CoverImageAppService(
            CloudinaryManager cloudinaryManager,
           IProjectRepository projectRepository)
        {
            _cloudinaryManager = cloudinaryManager;
            _projectRepository = projectRepository;
        }

        public virtual async Task<Uri> UploadAsync(IFormFile file)
        {
            if (file != null)
            {
                string imagePath;
                imagePath = await AppExtensions.GetFilePathAsync(file);
                return _cloudinaryManager.UploadImage(imagePath);
            }
            return null;
        }
    }
}
