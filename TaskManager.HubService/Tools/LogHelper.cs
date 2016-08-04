using System;
using Abp.Dependency;
using TaskManager.BaseService.SystemRuntime;
using TaskManager.Errors;
using TaskManager.Errors.Dto;
using TaskManager.Logs;
using TaskManager.Logs.Dto;

namespace TaskManager.HubService.Tools
{
    /// <summary>
    /// 节点内部日志操作类
    /// </summary>
    public static class LogHelper
    {
        private readonly static ILogAppService LogAppService;
        private readonly static IErrorAppService ErrorAppService;
        static LogHelper()
        {
            LogAppService = IocManager.Instance.Resolve<ILogAppService>();
            ErrorAppService = IocManager.Instance.Resolve<IErrorAppService>();

        }

        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="input"></param>
        private static void AddLog(LogInput input)
        {
            try
            {
                LogAppService.Create(input);

            }
            catch (Exception ex)
            {
                //todo 记录日志文本
                throw;
            }
        }
        /// <summary>
        /// 添加错误日志
        /// </summary>
        /// <param name="input"></param>
        private static void AddError(ErrorInput input)
        {
            var log = new LogInput
            {
                LogType = input.ErrorType,
                Msg = input.Msg,
                TaskId = input.TaskId,
                NodeId = input.NodeId
            };
            try
            {
                AddLog(log);
                ErrorAppService.Create(input);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 添加节点日志
        /// </summary>
        /// <param name="msg"></param>
        public static void AddNodeLog(string msg)
        {
            var model = new LogInput()
            {
                LogType = (byte)EnumTaskLogType.SystemLog,
                Msg = msg,
                TaskId = 1,
                NodeId = GlobalConfig.NodeId
            };

            AddLog(model);
        }

        /// <summary>
        /// 添加节点错误日志
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        public static void AddNodeError(string msg, Exception ex)
        {
            if (ex == null)
                ex = new Exception();
            //todo 记录错误信息

            var model = new ErrorInput
            {
                ErrorType = (byte)EnumTaskLogType.SystemError,
                Msg = msg + " 错误信息:" + ex.Message + " 堆栈:" + ex.StackTrace,
                TaskId = 0,
                NodeId = GlobalConfig.NodeId
            };
            AddError(model);
        }

        /// <summary>
        /// 添加任务日志
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="taskId"></param>
        /// <param name="logType"></param>
        public static void AddTaskLog(string msg, int taskId, byte logType = (byte)EnumTaskLogType.CommonLog)
        {
            var log = new LogInput
            {
                LogType = (byte)EnumTaskLogType.CommonLog,
                Msg = msg,
                TaskId = taskId,
                NodeId = GlobalConfig.NodeId
            };
            AddLog(log);
        }

        /// <summary>
        /// 添加任务错误日志
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="taskId"></param>
        /// <param name="ex"></param>
        /// <param name="errorType"></param>
        public static void AddTaskError(string msg, int taskId, Exception ex, byte errorType = (byte)EnumTaskLogType.CommonError)
        {
            if (ex == null)
                ex = new Exception();
            //todo 记录错误信息

            var error = new ErrorInput
            {
                ErrorType = errorType,
                Msg = msg + " 错误信息:" + ex.Message + " 堆栈:" + ex.StackTrace,
                TaskId = taskId,
                NodeId = GlobalConfig.NodeId
            };
            AddError(error);
        }
    }
}
