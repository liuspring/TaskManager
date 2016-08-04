using System.Collections.Generic;
using Abp.Application.Services;
using TaskManager.Errors.Dto;

namespace TaskManager.Errors
{
    public interface IErrorAppService : IApplicationService
    {
        int Create(ErrorInput input);

        ErrorOutput Get(int id);

        List<ErrorListOutput> GetList(ErrorListInput input);

        int GetListTotal(ErrorListInput input);
    }
}
