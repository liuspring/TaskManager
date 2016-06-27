using Abp.Domain.Repositories;
using TaskManager.Logs.Dto;

namespace TaskManager.Logs
{
    public class LogAppService : TaskManagerAppServiceBase, ILogAppService
    {
        private readonly IRepository<Log, int> _logRepository;

        public LogAppService(IRepository<Log, int> logRepository)
        {
            _logRepository = logRepository;
        }
        public int Create(LogInput input)
        {
            var log = Log.Create(input.NodeId, input.TaskId, input.Msg, input.LogType);
            _logRepository.Insert(log);
            return log.Id;
        }
    }
}
