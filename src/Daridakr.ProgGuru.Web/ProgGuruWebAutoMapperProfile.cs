using AutoMapper;
using Daridakr.ProgGuru.Groups;
using Daridakr.ProgGuru.Projects;
using Daridakr.ProgGuru.Users.Courses;
using Daridakr.ProgGuru.Users.JobExperiences;
using Daridakr.ProgGuru.Users.Skills;

namespace Daridakr.ProgGuru.Web;

public class ProgGuruWebAutoMapperProfile : Profile
{
    public ProgGuruWebAutoMapperProfile()
    {
        // PROJECTS COMMON
        CreateMap<ProjectDto, CreateProjectDto>();

        // PROJECTS ADMIN
        CreateMap<ProjectDto, AdminUpdateProjectDto>();

        CreateMap<Pages.Projects.Admin.CreateModalModel.AdminCreateProjectViewModel, CreateProjectDto>();
        CreateMap<Pages.Projects.Admin.CreateModalModel.AdminCreateProjectViewModel, AdminUpdateProjectDto>();
        CreateMap<Pages.Projects.Admin.EditModalModel.AdminEditProjectViewModel, AdminUpdateProjectDto>();
        CreateMap<ProjectDto, Pages.Projects.Admin.EditModalModel.AdminEditProjectViewModel>();

        // PROJECTS USER
        CreateMap<ProjectDto, UpdateProjectDto>();

        CreateMap<Pages.Projects.CreateModalModel.CreateProjectViewModel, CreateProjectDto>();
        CreateMap<Pages.Projects.CreateModalModel.CreateProjectViewModel, UpdateProjectDto>();
        CreateMap<Pages.Projects.EditModalModel.EditProjectViewModel, UpdateProjectDto>();
        CreateMap<ProjectDto, Pages.Projects.EditModalModel.EditProjectViewModel>();

        // GROUPS
        CreateMap<Pages.Groups.CreateModalModel.CreateGroupViewModel, CreateGroupDto>();
        CreateMap<GroupDto, Pages.Groups.EditModalModel.EditGroupViewModel>();
        CreateMap<Pages.Groups.EditModalModel.EditGroupViewModel, UpdateGroupDto>();

        // USER SKILLS
        CreateMap<Pages.Users.Skills.CreateCommonSkillModalModel.CreateCommonSkillViewModel, CreateCommonSkillDto>();
        CreateMap<Pages.Users.Skills.CreateLanguageSkillModalModel.CreateLanguageSkillViewModel, CreateLanguageSkillDto>();
        CreateMap<Pages.Users.Skills.CreateProfSkillModalModel.CreateProfSkillViewModel, CreateProfSkillDto>();

        CreateMap<Pages.Users.Skills.EditLanguageSkillModalModel.EditLanguageSkillViewModel, UpdateLanguageSkillDto>();
        CreateMap<LanguageSkillDto, Pages.Users.Skills.EditLanguageSkillModalModel.EditLanguageSkillViewModel>();

        CreateMap<Pages.Users.Skills.EditProfSkillModalModel.EditProfSkillViewModel, UpdateProfSkillDto>();
        CreateMap<ProfSkillDto, Pages.Users.Skills.EditProfSkillModalModel.EditProfSkillViewModel>();

        // USER JOB EXPERIENCE
        CreateMap<Pages.Users.JobExperiences.EditJobExperienceModalModel.EditJobExperienceViewModel, UpdateJobExperienceDto>();
        CreateMap<JobExperienceDto, Pages.Users.JobExperiences.EditJobExperienceModalModel.EditJobExperienceViewModel>();

        // USER COURCES
        CreateMap<Pages.Users.Courses.EditCourseModalModel.EditCourceViewModel, UpdateUserCourseDto>();
        CreateMap<UserCourseDto, Pages.Users.Courses.EditCourseModalModel.EditCourceViewModel>();
    }
}
