using Daridakr.ProgGuru.CloudStorage;
using Daridakr.ProgGuru.Entities.Users.Courses;
using Daridakr.ProgGuru.Users.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Daridakr.ProgGuru.Users
{
    public class UserCourseAppService : ProgGuruAppService, IUserCourseAppService
    {
        private readonly IUserCourseRepository _courseRepository;
        private readonly UserCourseManager _courseManager;
        private readonly CloudinaryManager _cloudinaryManager;

        public UserCourseAppService(
            IUserCourseRepository courseRepository,
            UserCourseManager courseManager,
            CloudinaryManager cloudinaryManager)
        {
            _courseRepository = courseRepository;
            _courseManager = courseManager;
            _cloudinaryManager = cloudinaryManager;
        }

        public async Task<UserCourseDto> GetAsync(Guid id)
        {
            var course = await _courseRepository.GetAsync(id);
            return ObjectMapper.Map<UserCourse, UserCourseDto>(course);
        }

        public async Task<List<UserCourseDto>> GetListAsync()
        {
            var userCourses = await _courseRepository.GetListAsync();
            return userCourses
                .Select(item => new UserCourseDto
                {
                    Id = item.Id,
                    CreatorId = item.CreatorId,
                    Name = item.Name,
                    CoverImagePath = item.CoverImagePath,
                    Description = item.Description,
                    ReceivingYear = item.ReceivingYear,
                    CreationTime = item.CreationTime
                }).ToList();
        }

        public bool TryCreateAsync(CreateUserCourseDto input)
        {
            _courseManager.CreateAsync(
               input.CreatorId,
               input.Name,
               input.Description,
               input.CoverImagePath,
               input.ReceivingYear
               );

            return true;
        }

        public async Task<UserCourseDto> CreateAsync(CreateUserCourseDto input)
        {
            var userCourse = _courseManager.CreateAsync(
                input.CreatorId,
                input.Name,
                input.Description,
                input.CoverImagePath,
                input.ReceivingYear
                );

            await _courseRepository.InsertAsync(userCourse);

            return ObjectMapper.Map<UserCourse, UserCourseDto>(userCourse);
        }

        public async Task<bool> TryUpdateAsync(Guid id, UpdateUserCourseDto input)
        {
            var userCourse = await _courseRepository.GetAsync(id);

            _courseManager.UpdateAsync(
                userCourse,
                input.Name,
                input.Description,
                input.CoverImagePath
                );

            return true;
        }

        public async Task<UserCourseDto> UpdateAsync(Guid id, UpdateUserCourseDto input)
        {
            var userCourse = await _courseRepository.GetAsync(id);

            if (input.CoverImagePath != userCourse.CoverImagePath)
            {
                if (userCourse.CoverImagePath != "") _cloudinaryManager.DeleteImageFromCloud(userCourse.CoverImagePath);
            }

            userCourse = _courseManager.UpdateAsync(
                userCourse,
                input.Name,
                input.Description,
                input.CoverImagePath
                );

            return ObjectMapper.Map<UserCourse, UserCourseDto>(userCourse);
        }

        public async Task DeleteAsync(Guid id)
        {
            var userCourse = await _courseRepository.GetAsync(id);
            if (userCourse.CoverImagePath != "") _cloudinaryManager.DeleteImageFromCloud(userCourse.CoverImagePath);
            await _courseRepository.DeleteAsync(id);
        }
    }
}
