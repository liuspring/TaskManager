using System;
using TaskManager.Node.Model;
using TaskManager.Node.TaskManager.OpenOperator;
using TaskManager.Node.TaskManager.SystemRuntime;

namespace TaskManager.Node.TaskManager
{
    /// <summary>
    /// 基础dll任务
    /// </summary>
    public abstract class BaseDllTask : MarshalByRefObject, IDisposable
    {
        /// <summary>
        /// 是否运行在测试中
        /// </summary>
        public bool IsTesting = false;
        /// <summary>
        /// 任务的配置信息，类似项目app.config文件配置
        /// 测试时需要手工代码填写配置,线上环境需要在任务发布的时候配置
        /// </summary>
        public TaskAppConfigInfo AppConfig = new TaskAppConfigInfo();
        /// <summary>
        /// 任务底层运行时信息
        /// </summary>
        public TaskSystemRuntimeInfo SystemRuntimeInfo = new TaskSystemRuntimeInfo();
        /// <summary>
        /// 任务底层运行时操作类
        /// </summary>
        public TaskSystemRuntimeOperator SystemRuntimeOperator;
        /// <summary>
        /// 任务公开第三方使用的操作类
        /// </summary>
        public TaskOpenOperator OpenOperator;
        /// <summary>
        /// 任务安全释放类
        /// </summary>
        public TaskSafeDisposeOperator SafeDisposeOperator;


        public BaseDllTask()
        {
            SystemRuntimeOperator = new TaskSystemRuntimeOperator(this);
            OpenOperator = new TaskOpenOperator(this);
        }




        /*忽略默认的对象租用行为，以便“在主机应用程序域运行时始终”将对象保存在内存中.   
          这种机制将对象锁定到内存中，防止对象被回收，但只能在主机应用程序运行   
          期间做到这样。*/
        public override object InitializeLifetimeService()
        {
            return null;
        }
        /// <summary>
        /// 线上环境运行入口
        /// </summary>
        public void TryRun()
        {
            try
            {
                IsTesting = false;
                SystemRuntimeOperator.UpdateLastStartTime(DateTime.Now);
                Run();
                SystemRuntimeOperator.UpdateLastEndTime(DateTime.Now);
                SystemRuntimeOperator.UpdateTaskSuccess();
                SystemRuntimeOperator.AddLog(new LogModel
                {
                    Msg = "任务【" + SystemRuntimeInfo.TaskModel.TaskName + "】执行完毕",
                    TaskId = SystemRuntimeInfo.TaskModel.Id,
                    LogType = (byte)EnumTaskLogType.SystemLog,
                    CreationTime = DateTime.Now,
                    NodeId = SystemRuntimeInfo.TaskModel.NodeId
                });
            }
            catch (Exception exp)
            {
                SystemRuntimeOperator.UpdateTaskError(DateTime.Now);
                SystemRuntimeOperator.AddError(new ErrorModel
                {
                    Msg = ("错误:" + exp.Message + " 堆栈:" + exp.StackTrace),
                    TaskId = SystemRuntimeInfo.TaskModel.Id,
                    ErrorType = (byte)EnumTaskLogType.SystemError,
                    CreationTime = DateTime.Now,
                    NodeId = SystemRuntimeInfo.TaskModel.NodeId
                });
            }
        }

        /// <summary>
        /// 与第三方约定的运行接口方面
        /// </summary>
        public abstract void Run();

        /// <summary>
        /// 测试环境运行入口
        /// </summary>
        public virtual void TestRun()
        {
            IsTesting = true;
            Run();
        }

        /// <summary>
        /// 系统级稀有资源释放接口,及卸载回调接口
        /// </summary>
        public virtual void Dispose()
        {
            if (SafeDisposeOperator != null)
            {
                SafeDisposeOperator.DisposedState = TaskDisposedState.Disposing;
                SystemRuntimeOperator.AddLog(new LogModel
                {
                    Msg = "任务【" + SystemRuntimeInfo.TaskModel.TaskName + "】已设置资源释放状态(Disposing),并等待任务安全终止信号",
                    TaskId = SystemRuntimeInfo.TaskModel.Id,
                    LogType = (byte)EnumTaskLogType.SystemLog,
                    CreationTime = DateTime.Now,
                    NodeId = SystemRuntimeInfo.TaskModel.NodeId
                });
                SafeDisposeOperator.WaitDisposeFinished();
                SystemRuntimeOperator.AddLog(new LogModel
                {
                    Msg = "任务【" + SystemRuntimeInfo.TaskModel.TaskName + "】已安全终止结束",
                    TaskId = SystemRuntimeInfo.TaskModel.Id,
                    LogType = (byte)EnumTaskLogType.SystemLog,
                    CreationTime = DateTime.Now,
                    NodeId = SystemRuntimeInfo.TaskModel.NodeId
                });
            }
        }
    }
}
