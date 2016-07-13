using Common;

namespace TaskManager.HubService
{
    /// <summary>
    /// 任务命令名称
    /// </summary>
    public enum EnumTaskCommandName
    {
        [EnumDisplayName("关闭任务")]
        StopTask = 0,
        [EnumDisplayName("开启任务")]
        StartTask = 1,
        [EnumDisplayName("重启任务")]
        ReStartTask = 2,
        [EnumDisplayName("卸载任务")]
        UninstallTask = 3,
    }

    /// <summary>
    /// 任务命令状态
    /// </summary>
    public enum EnumTaskCommandState
    {
        [EnumDisplayName("未执行")]
        None = 0,
        [EnumDisplayName("执行错误")]
        Error = 1,
        [EnumDisplayName("成功执行")]
        Success = 2
    }

    /// <summary>
    /// 任务执行状态
    /// </summary>
    public enum EnumTaskState
    {
        [EnumDisplayName("停止")]
        Stop = 0,
        [EnumDisplayName("运行中")]
        Running = 1,
    }
}
