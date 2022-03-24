namespace Daridakr.ProgGuru;

public static class ProgGuruDomainErrorCodes
{
    public static class Projects
    {
        public const string CompletedProjectRequires = "ProgGuru:Project:0001";
        public const string CompletedProjectInvalidPublishDate = "ProgGuru:Project:0002";
        public const string CompletedProjectPublishDateLaterReleaseDate = "ProgGuru:Project:0003";
        public const string AbandonedProjectRequiresPublishDate = "ProgGuru:Project:0004";
        public const string AbandonedProjectInvalidPublishDate = "ProgGuru:Project:0005";
        public const string DuringProjectRequiresRealeseDate = "ProgGuru:Project:0006";
        public const string DuringProjectInvalidRealeseDate = "ProgGuru:Project:0007";
        public const string DuringProjectInvalidPublishDate = "ProgGuru:Project:0008";
        public const string NotGithubLinkForProjectE = "ProgGuru:Project:0009";
        public const string ProjectWithTheSameRepositoryAlreadyExists = "ProgGuru:Project:0010";
        public const string ProjectAlreadyExists = "ProgGuru:Project:0011";
        public const string AbandonedProjectShouldNotHaveReleaseDate = "ProgGuru:Project:0012";
        public const string ProjectDoesntExists = "ProgGuru:Project:0013";
    }

    public static class Groups
    {
        public const string GroupAlreadyExists = "ProgGuru:Group:0001";
        public const string GroupDoesntExists = "ProgGuru:Group:0002";
        public const string GroupWebsiteInvalid = "ProgGuru:Group:0003";
        public const string GroupIssueYearInvalid = "ProgGuru:Group:0004";
    }

    public static class Users
    {
        public const string UserDoesntExists = "ProgGuru:User:0001";
        public const string SkillAlreadyExists = "ProgGuru:User:0002";
        public const string BeginningYearInvalid = "ProgGuru:User:0003";
        public const string KnownledgeLevelInvalid = "ProgGuru:User:0004";
        public const string EndYearInvalid = "ProgGuru:User:0005";
        public const string BeginningDateOfJob = "ProgGuru:User:0006";
        public const string EndDateOfJobInvalid = "ProgGuru:User:0007";
        public const string ReceivingYearCourseInvalid = "ProgGuru:User:0008";
    }

    public static class Specializations
    {
        public const string SpecializationAlreadyExists = "ProgGuru:Specialization:0001";
    }

    public static class Subscriptions
    {
        public const string SubscriptionAlreadyExists = "ProgGuru:Subscriptions:0001";
    }

    public static class Tags
    {
        public const string TagAlreadyExist = "ProgGuru:Tag:0001";
        public const string EntityNotTaggable = "ProgGuru:Tag:0002";
    }

    public static class Ratings
    {
        public const string EntityCantHaveRating = "ProgGuru:Rating:0001";
    }

    public static class Reactions
    {
        public const string EntityCantHaveReaction = "ProgGuru:Reaction:0001";
    }

    public static class Blogs
    {
        public const string SlugAlreadyExists = "ProgGuru:Blog:0001";
    }

    public static class BlogPosts
    {
        public const string SlugAlreadyExist = "ProgGuru:BlogPost:0001";
    }

    public static class Comments
    {
        public const string EntityNotCommentable = "ProgGuru:Comments:0001";
    }

    public static class MediaDescriptors
    {
        public const string InvalidName = "ProgGuru:Media:0001";
        public const string EntityTypeDoesntExist = "ProgGuru:Media:0002";
    }
}
