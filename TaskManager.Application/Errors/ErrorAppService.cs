using System.Collections.Generic;
using System.Linq;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using TaskManager.Errors.Dto;

namespace TaskManager.Errors
{
    public class ErrorAppService : TaskManagerAppServiceBase, IErrorAppService
    {
        private readonly IRepository<Error, int> _errorRepository;

        public ErrorAppService(IRepository<Error, int> logRepository)
        {
            _errorRepository = logRepository;
        }
        public int Create(ErrorInput input)
        {
            var log = Error.Create(input.NodeId, input.TaskId, input.Msg, input.ErrorType);
            _errorRepository.Insert(log);
            return log.Id;
        }

        public ErrorOutput Get(int id)
        {
            var error = _errorRepository.Get(id);
            return error.MapTo<ErrorOutput>();
        }

        public List<ErrorListOutput> GetList(ErrorListInput input)
        {
            var errors = _errorRepository.GetAll();
            errors = errors.OrderBy(a => a.Id).Skip(input.iDisplayStart).Take(input.iDisplayLength);
            return errors.MapTo<List<ErrorListOutput>>();
        }

        public int GetListTotal(ErrorListInput input)
        {
            var errors = _errorRepository.GetAll();
            return errors.Count();
        }
    }
}
