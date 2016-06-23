using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Dependency;
using Common;
using TaskManager.Node.TaskManager;
using TaskManager.Node.TaskManager.SystemRuntime;
using TaskManager.Node.Tools;
using TaskManager.Tasks;
using TaskManager.Versions;
using Task = TaskManager.Tasks.Task;

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
            var taskruntimeinfo = TaskPoolManager.CreateInstance().Get(taskid.ToString());
            if (taskruntimeinfo != null)
            {
                throw new Exception("任务已在运行中");
            }

            taskruntimeinfo = new NodeTaskRuntimeInfo();
            taskruntimeinfo.TaskLock = new TaskLock();

            var task = _taskAppService.GetTask(taskid);
            taskruntimeinfo.TaskModel = task;
            var versionInfo = _versionInfoService.GetVersionInfo(taskid, taskruntimeinfo.TaskModel.Version);
            taskruntimeinfo.TaskVersionModel = versionInfo;
            string filelocalcachepath = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + "\\" + GlobalConfig.TaskDllCompressFileCacheDir + @"\" + taskruntimeinfo.TaskModel.Id + @"\" + taskruntimeinfo.TaskModel.Version + @"\" +
                taskruntimeinfo.TaskVersionModel.ZipFileName;
            string fileinstallpath = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + "\\" + GlobalConfig.TaskDllDir + @"\" + taskruntimeinfo.TaskModel.Id;
            string fileinstallmainclassdllpath = fileinstallpath + @"\" + taskruntimeinfo.TaskModel.MainClassDllFileName;
            string taskshareddlldir = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + "\\" + GlobalConfig.TaskSharedDllsDir;

            IoHelper.CreateDirectory(filelocalcachepath);
            IoHelper.CreateDirectory(fileinstallpath);
            File.WriteAllBytes(filelocalcachepath, taskruntimeinfo.TaskVersionModel.ZipFile);

            CompressHelper.UnCompress(filelocalcachepath, fileinstallpath);
            //拷贝共享程序集
            IoHelper.CopyDirectory(taskshareddlldir, fileinstallpath);
            try
            {
                var dlltask = new AppDomainLoader<BaseDllTask>().Load(fileinstallmainclassdllpath, taskruntimeinfo.TaskModel.MainClassNameSpace, out taskruntimeinfo.Domain);
                dlltask.SystemRuntimeInfo = new TaskSystemRuntimeInfo()
                {
                    TaskConnectString = GlobalConfig.TaskDataBaseConnectString,
                    TaskModel = taskruntimeinfo.TaskModel
                };

                dlltask.AppConfig = new TaskAppConfigInfo();
                if (!string.IsNullOrEmpty(taskruntimeinfo.TaskModel.AppConfigJson))
                {
                    dlltask.AppConfig =
                        JsonHelper.DeSerialize<TaskAppConfigInfo>(taskruntimeinfo.TaskModel.AppConfigJson);
                }
                taskruntimeinfo.DllTask = dlltask;
                bool r = TaskPoolManager.CreateInstance().Add(taskid.ToString(), taskruntimeinfo);
                _taskAppService.UpdateTaskState(taskid,(int)EnumTaskState.Running);
                LogHelper.AddTaskLog("节点开启任务成功", taskid);
                return r;
            }
            catch (Exception exp)
            {
                DisposeTask(taskid, taskruntimeinfo, true);
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
            _taskAppService.UpdateTaskState(taskid,(int)EnumTaskState.Stop);
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
            _taskAppService.UpdateTaskState(taskid, (int)EnumTaskState.Stop);
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
