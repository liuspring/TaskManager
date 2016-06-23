using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Node.TaskManager.SystemRuntime
{
    public enum EnumTaskLogType
    {
        [Description("常用日志")]
        CommonLog = 1,
        [Description("系统日志")]
        SystemLog = 2,
        [Description("系统错误日志")]
        SystemError = 3,
        [Description("常用错误日志")]
        CommonError = 4,
    }
}
