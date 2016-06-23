using System;
using Common;
using TaskManager.Node.Dal;
using TaskManager.Node.Model;
using XXF.Extensions;
using XXF.ProjectTool;

namespace TaskManager.Node.TaskManager.SystemRuntime
{
    /// <summary>
    /// 任务运行时底层操作类
    /// 仅平台内部使用
    /// </summary>
    public class TaskSystemRuntimeOperator
    {
        /// <summary>
        /// 任务dll实例引用
        /// </summary>
        protected BaseDllTask DllTask = null;

        protected string Localtempdatafilename = "localtempdata.json.txt";

        public TaskSystemRuntimeOperator(BaseDllTask dlltask)
        {
            DllTask = dlltask;
        }

        public void SaveLocalTempData(object obj)
        {
            string filename = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + "\\" + Localtempdatafilename;
            var json = JsonHelper.Serialize(obj);
            System.IO.File.WriteAllText(filename, json);
        }
        public T GetLocalTempData<T>() where T : class
        {
            string filename = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + "\\" + Localtempdatafilename;
            if (!System.IO.File.Exists(filename))
                return null;
            string content = System.IO.File.ReadAllText(filename);
            var obj = JsonHelper.DeSerialize<T>(content);
            return obj;
        }
        public void SaveDataBaseTempData(object obj)
        {
            SqlHelper.ExcuteSql(DllTask.SystemRuntimeInfo.TaskConnectString, (c) =>
            {
                var tempDataDal = new TempDataDal();
                tempDataDal.SaveTempData(c, DllTask.SystemRuntimeInfo.TaskModel.Id, JsonHelper.Serialize(obj));
            });
        }
        public T GetDataBaseTempData<T>() where T : class
        {
            string json = null;
            SqlHelper.ExcuteSql(DllTask.SystemRuntimeInfo.TaskConnectString, (c) =>
            {
                var tempDataDal = new TempDataDal();

                json = tempDataDal.GetTempData(c, DllTask.SystemRuntimeInfo.TaskModel.Id);
            });
            if (string.IsNullOrEmpty(json))
            {
                return null;
            }
            var obj = JsonHelper.DeSerialize<T>(json);
            return obj;
        }

        public void UpdateLastStartTime(DateTime time)
        {
            SqlHelper.ExcuteSql(DllTask.SystemRuntimeInfo.TaskConnectString, (c) =>
            {
                var taskDal = new TaskDal();
                taskDal.UpdateLastStartTime(c, DllTask.SystemRuntimeInfo.TaskModel.Id, time);
            });
        }

        public void UpdateLastEndTime(DateTime time)
        {
            SqlHelper.ExcuteSql(DllTask.SystemRuntimeInfo.TaskConnectString, (c) =>
            {
                var taskDal = new TaskDal();
                taskDal.UpdateLastEndTime(c, DllTask.SystemRuntimeInfo.TaskModel.Id, time);
            });
        }

        public void UpdateTaskError(DateTime time)
        {
            SqlHelper.ExcuteSql(DllTask.SystemRuntimeInfo.TaskConnectString, (c) =>
            {
                var taskDal = new TaskDal();
                taskDal.UpdateTaskError(c, DllTask.SystemRuntimeInfo.TaskModel.Id, time);
            });
        }

        public void UpdateTaskSuccess()
        {
            SqlHelper.ExcuteSql(DllTask.SystemRuntimeInfo.TaskConnectString, (c) =>
            {
                var taskDal = new TaskDal();
                taskDal.UpdateTaskSuccess(c, DllTask.SystemRuntimeInfo.TaskModel.Id);
            });
        }

        public void AddLog(LogModel model)
        {
            SqlHelper.ExcuteSql(DllTask.SystemRuntimeInfo.TaskConnectString, (c) =>
            {
                var logDal = new LogDal();
                model.Msg = model.Msg.SubString2(1000);
                logDal.Add(c, model);
            });
        }

        public void AddError(ErrorModel model)
        {
            AddLog(new LogModel()
            {
                CreationTime = model.CreationTime,
                LogType = model.ErrorType,
                Msg = model.Msg,
                TaskId = model.TaskId
            });
            SqlHelper.ExcuteSql(DllTask.SystemRuntimeInfo.TaskConnectString, (c) =>
            {
                var errorDal = new ErrorDal();
                model.Msg = model.Msg.SubString2(1000);
                errorDal.Add(c, model);
            });
        }
    }
}
