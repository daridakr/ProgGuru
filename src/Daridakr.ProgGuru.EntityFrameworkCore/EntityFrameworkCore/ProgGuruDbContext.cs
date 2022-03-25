using Daridakr.ProgGuru.Consts;
using Daridakr.ProgGuru.Entities.Groups;
using Daridakr.ProgGuru.Entities.Projects;
using Daridakr.ProgGuru.Entities.Subscriptions;
using Daridakr.ProgGuru.Entities.Users.Courses;
using Daridakr.ProgGuru.Entities.Users.JobExperiences;
using Daridakr.ProgGuru.Entities.Users.Skills;
using Daridakr.ProgGuru.Entities.Users.Specializations;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.IdentityServer.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;
using Volo.CmsKit.EntityFrameworkCore;

namespace Daridakr.ProgGuru.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class ProgGuruDbContext :
    AbpDbContext<ProgGuruDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    #region Entities from the modules

    /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityDbContext and ITenantManagementDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    //Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }

    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion

    // ProgGuru Entities

    public DbSet<Project> Projects { get; set; }

    public DbSet<Group> Groups { get; set; }

    public DbSet<GroupSubscription> GroupSubscriptions { get; set; }

    public DbSet<UserSubscription> UserSubscriptions { get; set; }

    public DbSet<Specialization> Specializations { get; set; }

    public DbSet<UserCommonSkill> CommonUserSkills { get; set; }

    public DbSet<UserLanguageSkill> LanguageUserSkills { get; set; }

    public DbSet<UserProfSkill> ProfUserSkills { get; set; }

    public DbSet<UserCourse> User—ourses { get; set; }

    public DbSet<UserJobExperience> UserJobExperiences { get; set; }

    public ProgGuruDbContext(DbContextOptions<ProgGuruDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureIdentityServer();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();

        builder.Entity<Project>(project =>
        {
            project.ToTable("Projects");

            project.ConfigureByConvention(); //auto configure for the base class props, method gracefully configures/maps the inherited properties

            project.Property(p => p.Title).IsRequired().HasMaxLength(PostConsts.MaxTitleLength);
            project.Property(p => p.Subtitle).IsRequired().HasMaxLength(PostConsts.MaxSubtitleLength);
            project.Property(p => p.TextInformation).IsRequired().HasMaxLength(PostConsts.MaxTextInformationLength);
            project.Property(p => p.Category).IsRequired();
            project.Property(p => p.Status).IsRequired();
            project.HasOne<Group>().WithMany().HasForeignKey(x => x.GroupId).IsRequired();

            project.ApplyObjectExtensionMappings();
        });

        builder.Entity<Group>(group =>
        {
            group.ToTable("Groups");
            group.ConfigureByConvention();
            group.Property(x => x.Title).IsRequired().HasMaxLength(GroupConsts.MaxTitleLength);
            group.Property(x => x.Subtitle).IsRequired().HasMaxLength(GroupConsts.MaxSubtitleLength);
            group.Property(x => x.TextInformation).IsRequired().HasMaxLength(GroupConsts.MaxTextInformationLength);
            group.Property(x => x.Developer).IsRequired().HasMaxLength(GroupConsts.MaxDeveloperLength);
            group.Property(x => x.IssueYear).IsRequired().HasMaxLength(GroupConsts.MaxIssueYearLength);
            group.Property(x => x.Website).HasMaxLength(GroupConsts.MaxWebsiteLength);
            group.HasIndex(x => x.Title);
        });

        builder.Entity<GroupSubscription>(groupSubscription =>
        {
            groupSubscription.ToTable("GroupSubscriptions");
            groupSubscription.ConfigureByConvention();
            groupSubscription.Property(x => x.CreatorId).IsRequired();
            groupSubscription.HasOne<Group>().WithMany().HasForeignKey(x => x.GroupId).IsRequired();
        });

        builder.Entity<UserSubscription>(userSubscription =>
        {
            userSubscription.ToTable("UserSubscriptions");
            userSubscription.ConfigureByConvention();
            userSubscription.Property(x => x.CreatorId).IsRequired();
            userSubscription.HasOne<IdentityUser>().WithMany().HasForeignKey(x => x.UserId).IsRequired();
        });

        builder.Entity<Specialization>(specialization =>
        {
            specialization.ToTable("Specializations");
            specialization.ConfigureByConvention();
            specialization.Property(x => x.Name).IsRequired().HasMaxLength(UserConsts.MaxSpecializationLength);
        });

        builder.Entity<UserCommonSkill>(skill =>
        {
            skill.ToTable("CommonUserSkills");
            skill.ConfigureByConvention();
            skill.Property(x => x.CreatorId).IsRequired();
            skill.Property(x => x.Name).IsRequired().HasMaxLength(UserConsts.MaxSkillNameLength);
        });

        builder.Entity<UserLanguageSkill>(languageSkill =>
        {
            languageSkill.ToTable("LanguageUserSkills");
            languageSkill.ConfigureByConvention();
            languageSkill.Property(x => x.CreatorId).IsRequired();
            languageSkill.Property(x => x.Name).IsRequired().HasMaxLength(UserConsts.MaxSkillNameLength);
            languageSkill.Property(x => x.LanguageLevel).IsRequired();
        });

        builder.Entity<UserProfSkill>(profSkill =>
        {
            profSkill.ToTable("ProfUserSkills");
            profSkill.ConfigureByConvention();
            profSkill.Property(x => x.CreatorId).IsRequired();
            profSkill.Property(x => x.Name).IsRequired().HasMaxLength(UserConsts.MaxSkillNameLength);
            profSkill.HasOne<Group>().WithMany().HasForeignKey(x => x.GroupId).IsRequired();
            profSkill.Property(x => x.BeginningYear).IsRequired();
            profSkill.Property(x => x.KnownledgeLevel).IsRequired();
        });

        builder.Entity<UserCourse>(course =>
        {
            course.ToTable("User—ourses");
            course.ConfigureByConvention();

            course.Property(x => x.CreatorId).IsRequired();
            course.Property(x => x.Name).IsRequired().HasMaxLength(UserConsts.MaxCourseLength);
            course.Property(x => x.Description).HasMaxLength(UserConsts.MaxCourseDescriptionLength);
            course.Property(x => x.ReceivingYear).IsRequired();
        });

        builder.Entity<UserJobExperience>(jobExperience =>
        {
            jobExperience.ToTable("UserJobExperiences");
            jobExperience.ConfigureByConvention();

            jobExperience.Property(x => x.CreatorId).IsRequired();
            jobExperience.Property(x => x.Position).IsRequired().HasMaxLength(UserConsts.MaxPositionLength);
            jobExperience.Property(x => x.CompanyName).IsRequired().HasMaxLength(UserConsts.MaxCompanyNameLength);
            jobExperience.Property(x => x.Location).HasMaxLength(UserConsts.MaxLocationLength);
            jobExperience.Property(x => x.BeginningDate).IsRequired();
        });

        builder.ConfigureCmsKit();
    }
}
