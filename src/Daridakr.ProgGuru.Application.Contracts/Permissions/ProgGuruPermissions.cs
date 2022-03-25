namespace Daridakr.ProgGuru.Permissions;

public static class ProgGuruPermissions
{
    public const string GroupName = "ProgGuru";

    public static class Users
    {
        public const string Default = GroupName + ".Users";
        public const string View = Default + ".View";
    }

    public static class Projects
    {
        public const string Default = GroupName + ".Projects";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public static class Groups
    {
        public const string Default = GroupName + ".Groups";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
        public const string View = Default + ".View";
    }

    public static class Subscriptions
    {
        public const string Default = GroupName + ".Subscriptions";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
        public const string View = Default + ".View";
    }
}
