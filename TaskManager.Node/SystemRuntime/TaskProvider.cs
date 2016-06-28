using System;
using System.IO;
using Abp.Dependency;
using Common;
using TaskManager.Node.TaskManager;
using TaskManager.Node.TaskManager.SystemRuntime;
using TaskManager.Node.Tools;
using TaskManager.Tasks;
using TaskManager.Versions;

namespace TaskManager.Node.SystemRuntime
{
    /// <summary>
    /// 任务操作提供者
    /// 提供任务的开始，关闭,重启，卸载
    /// </summary>
    public class TaskProvider
    {
        private readonly ITaskAppService _taskAppService;
        private readonly IVersionInfoService _versionInfoService;


        public TaskProvider()
        {
            _taskAppService = IocManager.Instance.Resolve<ITaskAppService>();
            _versionInfoService = IocManager.Instance.Resolve<IVersionInfoService>();
        }

        /// <summary>
        /// 任务的开启
        /// </summary>
        /// <param name="taskid"></param>
        /// <returns></returns>
        public bool Start(int taskid)
        {
            var taskRuntimeInfo = TaskPoolManager.CreateInstance().Get(taskid.ToString());
            if (taskRuntimeInfo != null)
            {
                throw new Exception("任务已在运行中");
            }

            taskRuntimeInfo = new NodeTaskRuntimeInfo
            {
                TaskLock = new TaskLock(),
                TaskModel = _taskAppService.GetTaskInfo(taskid)
            };
            taskRuntimeInfo.TaskVersionModel = _versionInfoService.GetVersionInfo(taskid, taskRuntimeInfo.TaskModel.Version);

            string fileLocalCachePath = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + "\\" + GlobalConfig.TaskDllCompressFileCacheDir + @"\" + taskRuntimeInfo.TaskModel.Id + @"\" + taskRuntimeInfo.TaskModel.Version + @"\" +
                taskRuntimeInfo.TaskVersionModel.ZipFileName;
            string fileInstallPath = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + "\\" + GlobalConfig.TaskDllDir + @"\" + taskRuntimeInfo.TaskModel.Id;
            string fileInstallMainClassDllPath = fileInstallPath + @"\" + taskRuntimeInfo.TaskModel.MainClassDllFileName;
            string taskShareDllDir = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + "\\" + GlobalConfig.TaskSharedDllsDir;

            //IoHelper.CreateDirectory(fileLocalCachePath);
            //IoHelper.CreateDirectory(fileInstallPath);
            //File.WriteAllBytes(fileLocalCachePath, taskRuntimeInfo.TaskVersionModel.ZipFile);

            //CompressHelper.UnCompress(fileLocalCachePath, fileInstallPath);
            //拷贝共享程序集
            //IoHelper.CopyDirectory(taskShareDllDir, fileInstallPath);
            try
            {
                var dllTask = new AppDomainLoader<BaseDllTask>().Load(fileInstallMainClassDllPath,
                    taskRuntimeInfo.TaskModel.MainClassNameSpace,
                    out taskRuntimeInfo.Domain);

                var taskSystemRuntimeInfo=new TaskSystemRuntimeInfo();
                taskSystemRuntimeInfo.TaskModel = taskRuntimeInfo.TaskModel;
                //dllTask.SystemRuntimeInfo = new TaskSystemRuntimeInfo()
                //{
                //    //TaskConnectString = GlobalConfig.TaskDataBaseConnectString,
                //    TaskModel = taskRuntimeInfo.TaskModel
                //};

                dllTask.AppConfig = new TaskAppConfigInfo();
                if (!string.IsNullOrEmpty(taskRuntimeInfo.TaskModel.AppConfigJson))
                {
                    dllTask.AppConfig =
                        JsonHelper.DeSerialize<TaskAppConfigInfo>(taskRuntimeInfo.TaskModel.AppConfigJson);
                }
                taskRuntimeInfo.DllTask = dllTask;
                bool r = TaskPoolManager.CreateInstance().Add(taskid.ToString(), taskRuntimeInfo);
                _taskAppService.UpdateTaskState(taskid, (byte)EnumTaskState.Running);
                LogHelper.AddTaskLog("节点开启任务成功", taskid);
                return r;
            }
            catch (Exception exp)
            {
                DisposeTask(taskid, taskRuntimeInfo, true);
                throw exp;
            }
        }
        /// <summary>
        /// 任务的关闭
        /// </summary>
        /// <param name="taskid"></param>
        /// <returns></returns>
        public bool Stop(int taskid)
        {
            var taskruntimeinfo = TaskPoolManager.CreateInstance().Get(taskid.ToString());
            if (taskruntimeinfo == null)
            {
                throw new Exception("任务不在运行中");
            }

            var r = DisposeTask(taskid, taskruntimeinfo, false);
            _taskAppService.UpdateTaskState(taskid, (byte)EnumTaskState.Stop);
            LogHelper.AddTaskLog("节点关闭任务成功", taskid);
            return r;
        }
        /// <summary>
        /// 任务的卸载
        /// </summary>
        /// <param name="taskid"></param>
        /// <returns></returns>
        public bool Uninstall(int taskid)
        {
            var taskruntimeinfo = TaskPoolManager.CreateInstance().Get(taskid.ToString());
            if (taskruntimeinfo == null)
            {
                throw new Exception("任务不在运行中");
            }
            var r = DisposeTask(taskid, taskruntimeinfo, true);
            _taskAppService.UpdateTaskState(taskid, (byte)EnumTaskState.Stop);
            LogHelper.AddTaskLog("节点卸载任务成功", taskid);
            return r;
        }

        /// <summary>
        /// 任务的资源释放
        /// </summary>
        /// <param name="taskid"></param>
        /// <param name="taskruntimeinfo"></param>
        /// <param name="isforceDispose"></param>
        /// <returns></returns>
        private bool DisposeTask(int taskid, NodeTaskRuntimeInfo taskruntimeinfo, bool isforceDispose)
        {
            if (taskruntimeinfo != null && taskruntimeinfo.DllTask != null)
                try
                {
                    taskruntimeinfo.DllTask.Dispose();
                    taskruntimeinfo.DllTask = null;
                }
                catch (TaskSafeDisposeTimeOutException ex)
                {
                    LogHelper.AddNodeError("强制资源释放之任务资源释放", ex);
                    if (isforceDispose == false)
                        throw ex;
                }
                catch (Exception e) { LogHelper.AddNodeError("强制资源释放之任务资源释放", e); }
            if (taskruntimeinfo != null && taskruntimeinfo.Domain != null)
                try
                {
                    new AppDomainLoader<BaseDllTask>().UnLoad(taskruntimeinfo.Domain); taskruntimeinfo.Domain = null;
                }
                catch (Exception e) { LogHelper.AddNodeError("强制资源释放之应用程序域释放", e); }
            if (TaskPoolManager.CreateInstance().Get(taskid.ToString()) != null)
                try
                {
                    TaskPoolManager.CreateInstance().Remove(taskid.ToString());
                }
                catch (Exception e)
                {
                    LogHelper.AddNodeError("强制资源释放之任务池释放", e);
                }
            LogHelper.AddTaskLog("节点已对任务进行资源释放", taskid);
            return true;
        }
    }
}
