using System.Collections.Generic;
using Abp.Application.Services;
using TaskManager.Logs.Dto;

namespace TaskManager.Logs
{
    public interface ILogAppService : IApplicationService
    {
        int Create(LogInput input);

        LogOutput Get(int id);

        List<LogListOutput> GetList(LogListInput input);

        int GetListTotal(LogListInput input);

    }
}
