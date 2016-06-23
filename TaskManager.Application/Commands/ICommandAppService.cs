using System.Collections.Generic;
using Abp.Application.Services;
using TaskManager.Commands.Dto;

namespace TaskManager.Commands
{
    public interface ICommandAppService : IApplicationService
    {
        int Create(CommandInput input);

        CommandOutput GetCommand(int id);

        void Update(CommandInput input);

        List<CommandListOutput> GetList(CommandListInput input);

        int GetListTotal(CommandListInput input);
    }
}
