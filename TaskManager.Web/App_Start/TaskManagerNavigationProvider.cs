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
                        new LocalizableString("节点管理", TaskManagerConsts.LocalizationSourceName),
                        url: "/nodes/index",
                        icon: "fa fa-circle-o"
                        ))
                ).AddItem(
                    new MenuItemDefinition(
                        "日志查看",
                        new LocalizableString("日志查看", TaskManagerConsts.LocalizationSourceName),
                        url: "",
                        icon: "fa fa-users"
                        ).AddItem(new MenuItemDefinition(
                        "一般日志",
                        new LocalizableString("一般日志", TaskManagerConsts.LocalizationSourceName),
                        url: "/logs/index",
                        icon: "fa fa-circle-o"
                        )).AddItem(new MenuItemDefinition(
                        "错误日志",
                        new LocalizableString("错误日志", TaskManagerConsts.LocalizationSourceName),
                        url: "/errors/index",
                        icon: "fa fa-circle-o"
                        ))
                ).AddItem(
                    new MenuItemDefinition(
                        "性能查看",
                        new LocalizableString("性能查看", TaskManagerConsts.LocalizationSourceName),
                        url: "",
                        icon: "fa fa-users"
                        ).AddItem(new MenuItemDefinition(
                        "任务性能分析",
                        new LocalizableString("任务性能分析", TaskManagerConsts.LocalizationSourceName),
                        url: "/logs/index",
                        icon: "fa fa-circle-o"
                        )).AddItem(new MenuItemDefinition(
                        "节点性能分析",
                        new LocalizableString("节点性能分析", TaskManagerConsts.LocalizationSourceName),
                        url: "/errors/index",
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
