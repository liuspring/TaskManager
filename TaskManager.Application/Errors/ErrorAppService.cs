using Abp.Domain.Repositories;
using TaskManager.Errors.Dto;

namespace TaskManager.Errors
{
    public class ErrorAppService : TaskManagerAppServiceBase, IErrorAppService
    {
        private readonly IRepository<Error, int> _logRepository;

        public ErrorAppService(IRepository<Error, int> logRepository)
        {
            _logRepository = logRepository;
        }
        public int Create(ErrorInput input)
        {
            var log = Error.Create(input.NodeId, input.TaskId, input.Msg, input.ErrorType);
            _logRepository.Insert(log);
            return log.Id;
        }
    }
}
