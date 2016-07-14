using System.ComponentModel;

namespace TaskManager.HubService.Model
{
    public enum EnumTimeWatchLogType
    {
        [Description("API")]
        ApiUrl = 2,
        [Description("普通")]
        Common = 1,
        [Description("无")]
        None = 0,
        [Description("SQL")]
        SqlCmd = 3
    }
}
