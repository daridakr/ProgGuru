using AutoMapper;
using Daridakr.ProgGuru.Entities.Groups;
using Daridakr.ProgGuru.Entities.Projects;
using Daridakr.ProgGuru.Entities.Subscriptions;
using Daridakr.ProgGuru.Entities.Users.Courses;
using Daridakr.ProgGuru.Entities.Users.JobExperiences;
using Daridakr.ProgGuru.Entities.Users.Skills;
using Daridakr.ProgGuru.Entities.Users.Specializations;
using Daridakr.ProgGuru.Groups;
using Daridakr.ProgGuru.Projects;
using Daridakr.ProgGuru.Subscriptions;
using Daridakr.ProgGuru.Users.Courses;
using Daridakr.ProgGuru.Users.JobExperiences;
using Daridakr.ProgGuru.Users.Skills;
using Daridakr.ProgGuru.Users.Specializations;
using Volo.Abp.Identity;

namespace Daridakr.ProgGuru;

public class ProgGuruApplicationAutoMapperProfile : Profile
{
    public ProgGuruApplicationAutoMapperProfile()
    {
        CreateMap<Project, ProjectDto>();
        CreateMap<CreateProjectDto, Project>();
        CreateMap<UpdateProjectDto, Project>();
        CreateMap<AdminUpdateProjectDto, Project>();

        CreateMap<Group, GroupDto>();
        CreateMap<Group, GroupLookupDto>();

        CreateMap<IdentityUser, IdentityUserDto>().MapExtraProperties();
        CreateMap<IdentityUser, UserLookupDto>();

        CreateMap<Specialization, SpecializationDto>();

        CreateMap<UserCommonSkill, CommonSkillDto>();
        CreateMap<UserLanguageSkill, LanguageSkillDto>();
        CreateMap<UserProfSkill, ProfSkillDto>();

        CreateMap<CreateCommonSkillDto, UserCommonSkill>();
        CreateMap<CreateLanguageSkillDto, UserLanguageSkill>();
        CreateMap<CreateProfSkillDto, UserProfSkill>();

        CreateMap<UpdateCommonSkillDto, UserCommonSkill>();
        CreateMap<UpdateLanguageSkillDto, UserLanguageSkill>();
        CreateMap<UpdateProfSkillDto, UserProfSkill>();

        CreateMap<GroupSubscription, AdminGroupSubscriptionDto>();
        CreateMap<GroupSubscription, GroupSubscriberDto>();
        CreateMap<GroupSubscription, UserGroupSubscriptionDto>();
        CreateMap<CreateGroupSubscriptionDto, GroupSubscription>();

        CreateMap<UserSubscription, AdminUserSubscriptionDto>();
        CreateMap<UserSubscription, UserSubscriptionDto>();
        CreateMap<CreateUserSubscriptionDto, UserSubscription>();

        CreateMap<UserCourse, UserCourseDto>();
        CreateMap<CreateUserCourseDto, UserCourse>();
        CreateMap<UpdateUserCourseDto, UserCourse>();

        CreateMap<UserJobExperience, JobExperienceDto>();
        CreateMap<CreateJobExperienceDto, UserJobExperience>();
        CreateMap<UpdateJobExperienceDto, UserJobExperience>();
    }
}
