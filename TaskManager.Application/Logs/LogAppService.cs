using System.Collections.Generic;
using System.Linq;
using Abp.AutoMapper;
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

        public LogOutput Get(int id)
        {
            var log = _logRepository.Get(id);
            return log.MapTo<LogOutput>();
        }

        public List<LogListOutput> GetList(LogListInput input)
        {
            var logs = _logRepository.GetAll();
            logs = logs.OrderBy(a => a.Id).Skip(input.iDisplayStart).Take(input.iDisplayLength);
            return logs.MapTo<List<LogListOutput>>();
        }

        public int GetListTotal(LogListInput input)
        {
            var logs = _logRepository.GetAll();
            return logs.Count();
        }
    }
}
