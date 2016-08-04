using Common;


namespace TaskManager.Enum
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
    public enum EnumTaskLogType
    {
        [EnumDisplayName("未知")]
        None = 0,
        [EnumDisplayName("常用日志")]
        CommonLog = 1,
        [EnumDisplayName("系统日志")]
        SystemLog = 2,
        [EnumDisplayName("系统错误日志")]
        SystemError = 3,
        [EnumDisplayName("常用错误日志")]
        CommonError = 4,
    }

}
