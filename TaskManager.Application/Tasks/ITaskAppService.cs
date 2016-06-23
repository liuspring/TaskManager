using System.Collections.Generic;
using Abp.Application.Services;
using TaskManager.Tasks.Dto;

namespace TaskManager.Tasks
{
    public interface ITaskAppService : IApplicationService
    {
        int Create(CreateTaskInput input);

        List<TaskListOutput> GetList(TaskListInput input);

        int GetListTotal(TaskListInput input);

        List<Task> GetAllList();

        Task GetTask(int id);

        void UpdateTaskState(int taskId, int state);
    }
}
