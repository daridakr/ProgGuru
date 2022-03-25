using System.Threading.Tasks;
using Daridakr.ProgGuru.Localization;
using Daridakr.ProgGuru.MultiTenancy;
using Daridakr.ProgGuru.Permissions;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.TenantManagement.Web.Navigation;
using Volo.Abp.UI.Navigation;
using Volo.Abp.Users;

namespace Daridakr.ProgGuru.Web.Menus;

public class ProgGuruMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var administration = context.Menu.GetAdministration();
        
        var l = context.GetLocalizer<ProgGuruResource>();
        var currentUser = context.ServiceProvider.GetRequiredService<ICurrentUser>();

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                ProgGuruMenus.Home,
                l["Menu:Home"],
                "~/",
                icon: "fas fa-home",
                order: 0
            )
        );

        var progGuruMenu = new ApplicationMenuItem(
                "ProgGuru",
                 l["Menu:ProgGuru"],
                 icon: "fa fa-book"
            );

        var userMenu = new ApplicationMenuItem(
                l["userMenu"],
                 l["Menu:UserMenu"],
                 icon: "fa fa-wrench"
            );

        // ADMIN MANAGEMENT
        if (await context.IsGrantedAsync(ProgGuruPermissions.Groups.Create))
        {
            progGuruMenu.AddItem(new ApplicationMenuItem(
                "ProgGuru.Groups",
                l["Menu:Groups"],
                url: "/Groups"
            ));
            progGuruMenu.AddItem(new ApplicationMenuItem(
                "ProgGuru.Projects",
                l["Menu:Projects"],
                url: "/Projects"
            ));
            progGuruMenu.AddItem(new ApplicationMenuItem(
                "ProgGuru.Tags",
                l["Menu:Tags"],
                url: "/Cms/Tags"
            ));
            progGuruMenu.AddItem(new ApplicationMenuItem(
                "ProgGuru.Articles",
                l["Menu:Articles"],
                url: "/Cms/BlogPosts"
            ));
            progGuruMenu.AddItem(new ApplicationMenuItem(
                "ProgGuru.Comments",
                l["Menu:Comments"],
                url: "/Cms/Comments"
            ));
            progGuruMenu.AddItem(new ApplicationMenuItem(
                "ProgGuru.GroupSubscriptions",
                l["Menu:GroupSubscriptions"],
                url: "/group-subscriptions"
            ));
            progGuruMenu.AddItem(new ApplicationMenuItem(
                "ProgGuru.UserSubscriptions",
                l["Menu:UserSubscriptions"],
                url: "/user-subscriptions"
            ));
        }


        // USER MANAGEMENT
        //userMenu.AddItem(new ApplicationMenuItem(
        //        "ProgGuru.Projects",
        //        l["Menu:Projects"],
        //        url: "/my-projects"
        //    ));
        //

        if (currentUser.IsInRole("admin"))
        {
            administration.AddItem(progGuruMenu);
        }
        else
        {
            context.Menu.Items.Insert(1, userMenu);
            administration.Items.Clear();
        }

        if (MultiTenancyConsts.IsEnabled)
        {
            administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        }
        else
        {
            administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }

        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
        administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 3);
    }
}
