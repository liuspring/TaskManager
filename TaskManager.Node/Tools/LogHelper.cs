using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.EntityFramework;
using TaskManager.Logs;
using TaskManager.Node.TaskManager.SystemRuntime;

namespace TaskManager.Node.Tools
{
    /// <summary>
    /// 节点内部日志操作类
    /// </summary>
    public static class LogHelper
    {
        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="model"></param>
        private static void AddLog(Log model)
        {
            try
            {
                using (var context = new TaskManagerDbContext())
                {
                    var a = context.Categories.Count();


                    context.Logs.Add(model);
                    context.SaveChanges();
                }
            }
            catch (Exception exp)
            {
                //XXF.Log.ErrorLog.Write("添加日志至数据库出错", exp);
            }
        }
        /// <summary>
        /// 添加错误日志
        /// </summary>
        /// <param name="model"></param>
        private static void AddError(Errors.Error model)
        {
            try
            {
                AddLog(
                    new Log()
                {
                    LogType = model.ErrorType,
                    Msg = model.Msg,
                    TaskId = model.TaskId,
                    NodeId = GlobalConfig.NodeId
                });
                using (var context = new TaskManagerDbContext())
                {
                    context.Errors.Add(model);
                    context.SaveChanges();
                }

            }
            catch (Exception exp)
            {
                //XXF.Log.ErrorLog.Write("添加错误日志至数据库出错", exp);
            }
        }
        /// <summary>
        /// 添加节点日志
        /// </summary>
        /// <param name="msg"></param>
        public static void AddNodeLog(string msg)
        {
            //CommLog.Write(msg);
            var model = new Log()
            {
                LogType = (int)EnumTaskLogType.SystemLog,
                Msg = msg,
                TaskId = 0,
                NodeId = GlobalConfig.NodeId
            };
            //log model = new tb_log_model()
            //{
            //    logcreatetime = DateTime.Now,
            //    logtype = (byte)XXF.BaseService.TaskManager.SystemRuntime.EnumTaskLogType.SystemLog,
            //    msg = msg,
            //    taskid = 0,
            //    nodeid = GlobalConfig.NodeID
            //};
            AddLog(model);
        }
        /// <summary>
        /// 添加节点错误日志
        /// </summary>
        /// <param name="msg"></param>
        public static void AddNodeError(string msg, Exception exp)
        {
            //if (exp == null)
            //    exp = new Exception();
            //ErrorLog.Write(msg, exp);
            //tb_error_model model = new tb_error_model()
            //{
            //    errorcreatetime = DateTime.Now,
            //    errortype = (byte)XXF.BaseService.TaskManager.SystemRuntime.EnumTaskLogType.SystemError,
            //    msg = msg + " 错误信息:" + exp.Message + " 堆栈:" + exp.StackTrace,
            //    taskid = 0,
            //    nodeid = GlobalConfig.NodeID
            //};
            //AddError(model);
        }
        /// <summary>
        /// 添加任务日志
        /// </summary>
        /// <param name="msg"></param>
        public static void AddTaskLog(string msg, int taskid)
        {
            //tb_log_model model = new tb_log_model()
            //{
            //    logcreatetime = DateTime.Now,
            //    logtype = (byte)XXF.BaseService.TaskManager.SystemRuntime.EnumTaskLogType.CommonLog,
            //    msg = msg,
            //    taskid = taskid,
            //    nodeid = GlobalConfig.NodeID
            //};
            //AddLog(model);
        }
        /// <summary>
        /// 添加任务错误日志
        /// </summary>
        /// <param name="msg"></param>
        public static void AddTaskError(string msg, int taskid, Exception exp)
        {
            //ErrorLog.Write(msg + "[taskid:" + taskid + "]", exp);
            //if (exp == null)
            //    exp = new Exception();
            //tb_error_model model = new tb_error_model()
            //{
            //    errorcreatetime = DateTime.Now,
            //    errortype = (byte)XXF.BaseService.TaskManager.SystemRuntime.EnumTaskLogType.CommonError,
            //    msg = msg + " 错误信息:" + exp.Message + " 堆栈:" + exp.StackTrace,
            //    taskid = taskid,
            //    nodeid = GlobalConfig.NodeID
            //};
            //AddError(model);
        }
    }
}
