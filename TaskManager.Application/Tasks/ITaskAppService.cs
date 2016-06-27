using System;
using System.Collections.Generic;
using Abp.Application.Services;
using TaskManager.Tasks.Dto;

namespace TaskManager.Tasks
{
    public interface ITaskAppService : IApplicationService
    {
        int Create(TaskInput input);

        TaskOutput GetTask(int id);

        Task GetTaskInfo(int id);

        void Update(TaskInput input);

        List<TaskListOutput> GetList(TaskListInput input);

        int GetListTotal(TaskListInput input);

        List<Task> GetAllList();

        void UpdateTaskState(int taskId, byte state);

        List<Task> GetTasks(int nodeId, int state);

        void UpdateLastStartTime(int taskId, DateTime time);

        void UpdateLastEndTime(int taskId, DateTime time);

        void UpdateTaskError(int taskId, DateTime time);

        void UpdateTaskSuccess(int taskId);

    }
}
