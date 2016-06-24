using System.Collections.Generic;
using Abp.Application.Services;
using TaskManager.Tasks.Dto;

namespace TaskManager.Tasks
{
    public interface ITaskAppService : IApplicationService
    {
        int Create(TaskInput input);

        TaskOutput GetTask(int id);

        void Update(TaskInput input);

        List<TaskListOutput> GetList(TaskListInput input);

        int GetListTotal(TaskListInput input);

        List<Task> GetAllList();

        void UpdateTaskState(int taskId, int state);
    }
}
