using System;
using System.Linq;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using TaskManager.Performances.Dto;

namespace TaskManager.Performances
{
    public class PerformanceAppService : TaskManagerAppServiceBase, IPerformanceAppService
    {
        private readonly IRepository<Performance, int> _performanceRepository;

        public PerformanceAppService(IRepository<Performance, int> performanceRepository)
        {
            _performanceRepository = performanceRepository;
        }

        public int Create(PerformanceInput input)
        {
            var performance = Performance.Create(input.NodeId, input.TaskId, input.Cpu, input.Memory, input.InstallDirSize);
            _performanceRepository.Insert(performance);
            return performance.Id;
        }

        public PerformanceOutput GetPerformance(int taskId)
        {
            var performance = _performanceRepository.GetAll().FirstOrDefault(a => a.TaskId == taskId);
            performance = performance ?? new Performance();
            return performance.MapTo<PerformanceOutput>();
        }

        public void Update(PerformanceInput input)
        {
            var performance = _performanceRepository.GetAll().FirstOrDefault(a => a.TaskId == input.TaskId);
            if (performance != null)
            {
                performance.NodeId = input.NodeId;
                performance.TaskId = input.TaskId;
                performance.Cpu = input.Cpu;
                performance.Memory = input.Memory;
                performance.InstallDirSize = input.InstallDirSize;
            }
        }
    }
}
