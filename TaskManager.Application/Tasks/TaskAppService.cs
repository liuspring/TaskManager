using System;
using System.Collections.Generic;
using System.Linq;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Nito.AsyncEx;
using TaskManager.Tasks.Dto;
using TaskManager.TempDatas;
using TaskManager.Versions;


namespace TaskManager.Tasks
{
    public class TaskAppService : TaskManagerAppServiceBase, ITaskAppService
    {

        private readonly IRepository<Task, int> _taskRepository;
        private readonly IRepository<VersionInfo, int> _versionInfoRepository;
        private readonly IRepository<TempData, int> _tempDataRepository;


        public TaskAppService(IRepository<Task, int> taskRepository,
            IRepository<VersionInfo, int> versionInfoRepository,
            IRepository<TempData, int> tempDataRepository)
        {
            _taskRepository = taskRepository;
            _versionInfoRepository = versionInfoRepository;
            _tempDataRepository = tempDataRepository;
        }


        public int Create(TaskInput input)
        {
            var task = Task.Create(input.TaskName, input.CategoryId, input.NodeId, input.State,
                input.Version, input.AppConfigJson, input.Cron, input.MainClassDllFileName,
                input.MainClassNameSpace, input.Remark);
            _taskRepository.Insert(task);
            var versionInfo = VersionInfo.Create(task.Id, input.Version, input.FileZipName, input.FileZipPath);
            _versionInfoRepository.Insert(versionInfo);
            var tempData = TempData.Create(task.Id, input.TempDataJson);
            _tempDataRepository.Insert(tempData);
            return task.Id;
        }

        public TaskOutput GetTask(int id)
        {
            var task = _taskRepository.Get(id);
            var taskOutput = task.MapTo<TaskOutput>();
            var versionInfo = task.Versions.SingleOrDefault(a => a.TaskId == id && a.VersionType == task.Version);
            //var versionInfo = _versionInfoRepository.GetAll()
            //    .SingleOrDefault(a => a.TaskId == id && a.VersionType == task.Version);
            if (versionInfo != null)
            {
                taskOutput.Version = versionInfo.VersionType;
                taskOutput.FileZipName = versionInfo.ZipFileName;
                taskOutput.FileZipPath = versionInfo.ZipFilePath;
            }
            //var tempData = _tempDataRepository.GetAll().SingleOrDefault(a => a.TaskId == task.Id);
            var tempData = task.TempDatas.SingleOrDefault(a => a.TaskId == task.Id);
            if (tempData != null)
                taskOutput.TempDataJson = tempData.DataJson;
            return taskOutput;
        }

        public Task GetTaskInfo(int id)
        {
            return _taskRepository.Get(id);
        }

        public void Update(TaskInput input)
        {
            var task = _taskRepository.Get(input.Id);
            task.TaskName = input.TaskName;
            task.CategoryId = input.CategoryId;
            task.NodeId = input.NodeId;
            //task.State = input.State;
            task.Version = input.Version;
            task.AppConfigJson = input.AppConfigJson;
            task.Cron = input.Cron;
            task.MainClassDllFileName = input.MainClassDllFileName;
            task.MainClassNameSpace = input.MainClassNameSpace;
            task.Remark = input.Remark;

            //var versionInfo =
            //    _versionInfoRepository.GetAll()
            //        .SingleOrDefault(a => a.TaskId == input.Id && a.VersionType == input.Version);
            var versionInfo = task.Versions.SingleOrDefault(a => a.TaskId == task.Id && a.VersionType == task.Version);

            if (versionInfo == null)
            {
                versionInfo = VersionInfo.Create(task.Id, input.Version, input.FileZipName, input.FileZipPath);
                _versionInfoRepository.Insert(versionInfo);
            }
            else
            {
                versionInfo = VersionInfo.Update(versionInfo, input.FileZipName, input.FileZipPath);
                _versionInfoRepository.Update(versionInfo);
            }
            var tempData = task.TempDatas.SingleOrDefault(a => a.TaskId == task.Id);
            if (tempData == null)
            {
                tempData = TempData.Create(task.Id, input.TempDataJson);
                _tempDataRepository.Insert(tempData);
            }
            else
            {
                tempData.DataJson = input.TempDataJson;
                _tempDataRepository.Update(tempData);
            }
        }

        public List<TaskListOutput> GetList(TaskListInput input)
        {
            var tasks =
                _taskRepository.GetAll().
                Where(a => a.TaskName.Contains(input.TaskName)).
                OrderBy(a => a.Id).
                Skip(input.iDisplayStart).
                Take(input.iDisplayLength);
            return tasks.MapTo<List<TaskListOutput>>();
        }

        public int GetListTotal(TaskListInput input)
        {
            return _taskRepository.GetAll().Count(a => a.TaskName.Contains(input.TaskName));
        }

        public List<Task> GetAllList()
        {
            return _taskRepository.GetAllList();
        }

        public void UpdateTaskState(int taskId, byte state)
        {
            var task = _taskRepository.Get(taskId);
            task.State = state;
        }

        public List<Task> GetTasks(int nodeId, int state)
        {
            return _taskRepository.GetAll().Where(a => a.NodeId == nodeId && a.State == state).ToList();
        }

        public void UpdateLastStartTime(int taskId, DateTime time)
        {
            var task = _taskRepository.Get(taskId);
            task.LastStartTime = time;
        }

        public void UpdateLastEndTime(int taskId, DateTime time)
        {
            var task = _taskRepository.Get(taskId);
            task.LastEndTime = time;
        }

        public void UpdateTaskError(int taskId, DateTime time)
        {
            var task = _taskRepository.Get(taskId);
            task.LastErrorTime = time;
        }

        public void UpdateTaskSuccess(int taskId)
        {
            var task = _taskRepository.Get(taskId);
            task.ErrorCount = 0;
            task.RunCount = task.RunCount + 1;
        }
    }
}
