using System;
using Abp.Dependency;
using Common;
using TaskManager.Tasks;
using TaskManager.TempDatas;
using TaskManager.TempDatas.Dto;

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


        private readonly ITaskAppService _TaskAppService;
        private readonly ITempDataAppService _TempDataAppService;


        public TaskSystemRuntimeOperator(BaseDllTask dlltask)
        {
            DllTask = dlltask;
            _TaskAppService = IocManager.Instance.Resolve<ITaskAppService>();
            _TempDataAppService = IocManager.Instance.Resolve<ITempDataAppService>();
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
            var tempData = new TempDataInput
            {
                TaskId = DllTask.SystemRuntimeInfo.TaskModel.Id,
                DataJson = JsonHelper.Serialize(obj)
            };
            _TempDataAppService.Create(tempData);
        }
        public T GetDataBaseTempData<T>() where T : class
        {
            var tempData = _TempDataAppService.GetTempDataByTaskId(DllTask.SystemRuntimeInfo.TaskModel.Id);
            var json = tempData.DataJson;
            if (string.IsNullOrEmpty(json))
            {
                return null;
            }
            var obj = JsonHelper.DeSerialize<T>(json);
            return obj;
        }

        public void UpdateLastStartTime(DateTime time)
        {
            _TaskAppService.UpdateLastStartTime(DllTask.SystemRuntimeInfo.TaskModel.Id, time);
        }

        public void UpdateLastEndTime(DateTime time)
        {
            _TaskAppService.UpdateLastEndTime(DllTask.SystemRuntimeInfo.TaskModel.Id, time);
        }


        public void UpdateTaskError(DateTime time)
        {
            _TaskAppService.UpdateTaskError(DllTask.SystemRuntimeInfo.TaskModel.Id, time);
        }

        public void UpdateTaskSuccess()
        {
            _TaskAppService.UpdateTaskSuccess(DllTask.SystemRuntimeInfo.TaskModel.Id);
        }
    }
}
