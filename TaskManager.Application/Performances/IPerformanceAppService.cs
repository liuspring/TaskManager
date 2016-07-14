using Abp.Application.Services;
using TaskManager.Performances.Dto;

namespace TaskManager.Performances
{
    public interface IPerformanceAppService:IApplicationService
    {
        int Create(PerformanceInput input);

        PerformanceOutput GetPerformance(int taskId);

        void Update(PerformanceInput input);

    }
}
