using Daridakr.ProgGuru.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Daridakr.ProgGuru.Permissions;

public class ProgGuruPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        // Permission group
        var progGuruGroup = context.AddGroup(ProgGuruPermissions.GroupName, L("Permission:ProgGuru"));

        // Permissions inside this group
        /* Create, Edit and Delete are children of the ProgGuruPermissions.Projects.Default permission
         * A child permission can be selected only if the parent was selected */
        var projectsPermission = progGuruGroup.AddPermission(ProgGuruPermissions.Projects.Default, L("Permission:Projects"));
        projectsPermission.AddChild(ProgGuruPermissions.Projects.Create, L("Permission:Projects.Create"));
        projectsPermission.AddChild(ProgGuruPermissions.Projects.Edit, L("Permission:Projects.Edit"));
        projectsPermission.AddChild(ProgGuruPermissions.Projects.Delete, L("Permission:Projects.Delete"));

        var groupsPermission = progGuruGroup.AddPermission(ProgGuruPermissions.Groups.Default, L("Permission:Groups"));
        groupsPermission.AddChild(ProgGuruPermissions.Groups.Create, L("Permission:Groups.Create"));
        groupsPermission.AddChild(ProgGuruPermissions.Groups.Edit, L("Permission:Groups.Edit"));
        groupsPermission.AddChild(ProgGuruPermissions.Groups.Delete, L("Permission:Groups.Delete"));
        groupsPermission.AddChild(ProgGuruPermissions.Groups.View, L("Permission:Groups.View"));

        //var tagsPermission = progGuruGroup.AddPermission(ProgGuruPermissions.Tags.Default, L("Permission:Tags"));
        //tagsPermission.AddChild(ProgGuruPermissions.Tags.Create, L("Permission:Tags.Create"));
        //tagsPermission.AddChild(ProgGuruPermissions.Tags.Edit, L("Permission:Tags.Edit"));
        //tagsPermission.AddChild(ProgGuruPermissions.Tags.Delete, L("Permission:Tags.Delete"));

        var usersPermission = progGuruGroup.AddPermission(ProgGuruPermissions.Users.Default, L("Permission:Users"));
        usersPermission.AddChild(ProgGuruPermissions.Users.View, L("Permission:Users.View"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<ProgGuruResource>(name);
    }
}
