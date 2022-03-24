namespace Daridakr.ProgGuru.Consts
{
    public static class UserConsts
    {
        public const string EntityType = "User";

        // USER INFO
        public const int MaxBeginningYearLength = 4;
        public const int MaxLocationLength = 40;
        public const int MaxBioLength = 3780;
        public const int MaxSocialNetworkUsernameLength = 30;

        public const int MinLocationLength = 3;
        public const int MinBioLength = 100;
        public const int MinSocialNetworkUsernameLength = 2;

        public const int DiscordCodeLength = 4;

        public const string DefaultProfilePicture = "https://res.cloudinary.com/dgvd7jieo/image/upload/v1644954588/system/oazu4v5a7nsaxqtjd0mg.jpg";

        // SPECIALIZATIONS
        public const int MaxSpecializationLength = 40;
        public const int MinSpecializationLength = 5;

        // SKILLS
        public const int MaxSkillNameLength = 45;
        public const int MaxKnownledgeLevel = 5;

        public const int MinSkillNameLength = 1;
        public const int MinKnownledgeLevel = 1;

        // JOB EXPERIENCE
        public const int MaxPositionLength = MaxSpecializationLength;
        public const int MaxCompanyNameLength = 50;

        public const int MinPositionLength = MinSpecializationLength;
        public const int MinCompanyNameLength = 7;

        // COURSES
        public const int MaxCourseLength = 50;
        public const int MaxCourseDescriptionLength = 70;

        public const int MinCourseLength = 5;
        public const int MinCourseDescriptionLength = 15;
    }
}
