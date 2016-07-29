using Abp.Application.Navigation;
using Abp.Localization;
using TaskManager.Authorization;

namespace TaskManager.Web
{
    /// <summary>
    /// This class defines menus for the application.
    /// It uses ABP's menu system.
    /// When you add menu items here, they are automatically appear in angular application.
    /// See Views/Layout/_TopMenu.cshtml file to know how to render menu.
    /// </summary>
    public class TaskManagerNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            //context.Manager.MainMenu
            //    .AddItem(
            //        new MenuItemDefinition(
            //            "Home",
            //            L("HomePage"),
            //            url: "",
            //            icon: "fa fa-home",
            //            requiresAuthentication: true
            //            )
            //    ).AddItem(
            //        new MenuItemDefinition(
            //            "Tenants",
            //            L("Tenants"),
            //            url: "Tenants",
            //            icon: "fa fa-globe",
            //            requiredPermissionName: PermissionNames.Pages_Tenants
            //            )
            //    ).AddItem(
            //        new MenuItemDefinition(
            //            "Users",
            //            L("Users"),
            //            url: "Users",
            //            icon: "fa fa-users",
            //            requiredPermissionName: PermissionNames.Pages_Users
            //            )
            //    ).AddItem(
            //        new MenuItemDefinition(
            //            "About",
            //            L("About"),
            //            url: "About",
            //            icon: "fa fa-info"
            //            )
            //    );

            context.Manager.MainMenu
                .AddItem(
                    new MenuItemDefinition(
                        "任务管理",
                        new LocalizableString("任务管理", TaskManagerConsts.LocalizationSourceName),
                        url: "",
                        icon: "fa fa-calendar-check-o"
                        ).AddItem(new MenuItemDefinition(
                        "任务列表",
                        new LocalizableString("任务列表", TaskManagerConsts.LocalizationSourceName),
                        url: "/tasks/index",
                        icon: "fa fa-circle-o"
                        )).AddItem(new MenuItemDefinition(
                        "命令列表",
                        new LocalizableString("命令列表", TaskManagerConsts.LocalizationSourceName),
                        url: "/commands/index",
                        icon: "fa fa-circle-o"
                        )).AddItem(new MenuItemDefinition(
                        "分类列表",
                        new LocalizableString("分类列表", TaskManagerConsts.LocalizationSourceName),
                        url: "/categories/index",
                        icon: "fa fa-circle-o"
                        )).AddItem(new MenuItemDefinition(
                        "节点管理",
                        new LocalizableString("Events", TaskManagerConsts.LocalizationSourceName),
                        url: "/nodes/index",
                        icon: "fa fa-circle-o"
                        ))
                ).AddItem(
                    new MenuItemDefinition(
                        "迷失灵魂",
                        new LocalizableString("迷失灵魂", TaskManagerConsts.LocalizationSourceName),
                        url: "",
                        icon: "fa fa-users"
                        ).AddItem(new MenuItemDefinition(
                        "分类列表",
                        new LocalizableString("分类列表", TaskManagerConsts.LocalizationSourceName),
                        url: "/users/index",
                        icon: "fa fa-circle-o"
                        ))
                ).AddItem(
                    new MenuItemDefinition(
                        "用户管理",
                        new LocalizableString("用户管理", TaskManagerConsts.LocalizationSourceName),
                        url: "",
                        icon: "fa fa-users"
                        ).AddItem(new MenuItemDefinition(
                        "用户列表",
                        new LocalizableString("用户列表", TaskManagerConsts.LocalizationSourceName),
                        url: "/users/index",
                        icon: "fa fa-circle-o"
                        ))
                );
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, TaskManagerConsts.LocalizationSourceName);
        }
    }
}
