using System;
using TaskManager.Node.TaskManager.SystemRuntime;
using TaskManager.Node.Tools;

namespace TaskManager.Node.TaskManager.OpenOperator
{
    /// <summary>
    /// 任务公开给第三方使用操作类
    /// </summary>
    public class TaskOpenOperator
    {
        /// <summary>
        /// 任务dll实例引用
        /// </summary>
        protected BaseDllTask DllTask = null;

        public TaskOpenOperator(BaseDllTask dlltask)
        {
            DllTask = dlltask;
        }

        /// <summary>
        /// 获取当前任务安装目录
        /// </summary>
        /// <returns></returns>
        public string GetTaskInstallDirectory()
        {
            return AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + "\\";
        }

        /// <summary>
        /// 保存任务临时数据至本地文件 ".json.txt"
        /// </summary>
        /// <param name="obj"></param>
        public void SaveLocalTempData(object obj)
        {
            DllTask.SystemRuntimeOperator.SaveLocalTempData(obj);
            obj = null;
        }
        /// <summary>
        /// 从本地临时文件获取任务临时数据 ".json.txt"
        /// </summary>
        public T GetLocalTempData<T>() where T : class
        {
            return DllTask.SystemRuntimeOperator.GetLocalTempData<T>();
        }
        /// <summary>
        /// 保存任务临时数据至数据库(数据不能太大,也不能很频繁)
        /// </summary>
        /// <param name="obj"></param>
        public void SaveDataBaseTempData(object obj)
        {
            if (DllTask.IsTesting == false)
            {
                DllTask.SystemRuntimeOperator.SaveDataBaseTempData(obj);
                obj = null;
            }
        }
        /// <summary>
        /// 获取数据库任务临时数据
        /// </summary>
        public T GetDataBaseTempData<T>() where T : class
        {
            if (DllTask.IsTesting == false)
                return DllTask.SystemRuntimeOperator.GetDataBaseTempData<T>();
            else
                return null;
        }
        /// <summary>
        /// 写日志至线上数据库(不要频繁写日志，仅写一些便于分析的核心数据，或者非紧急的业务错误)
        /// </summary>
        /// <param name="msg"></param>
        public void Log(string msg)
        {
            LogHelper.AddTaskLog(msg, DllTask.SystemRuntimeInfo.TaskModel.Id, (byte)EnumTaskLogType.CommonLog);
        }
        /// <summary>
        /// 写错误日志至线上数据库,这些错误会通知到开发人员，所以不要写一些正常的业务错误
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        public void Error(string msg, Exception ex)
        {
            LogHelper.AddTaskError("错误信息", DllTask.SystemRuntimeInfo.TaskModel.Id, ex);
        }
    }
}
