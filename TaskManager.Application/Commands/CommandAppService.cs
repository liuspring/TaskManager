using System.Collections.Generic;
using System.Linq;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Castle.Windsor.Configuration.Interpreters.XmlProcessor.ElementProcessors;
using TaskManager.Authorization;
using TaskManager.Commands.Dto;

namespace TaskManager.Commands
{
    [AbpAuthorize(PermissionNames.Pages_Users)]
    public class CommandAppService : TaskManagerAppServiceBase, ICommandAppService
    {

        private readonly IRepository<Command, int> _commandRepository;

        public CommandAppService(IRepository<Command, int> commandRepository)
        {
            _commandRepository = commandRepository;
        }
        public int Create(CommandInput input)
        {
            var command = Command.Create(input.CommandName, input.CommandJson, input.CommandState, input.NodeId, input.TaskId);
            _commandRepository.Insert(command);
            return command.Id;
        }

        public CommandOutput GetCommand(int id)
        {
            var command = _commandRepository.Get(id);
            return command.MapTo<CommandOutput>();
        }

        public void Update(CommandInput input)
        {
            var command = _commandRepository.Get(input.Id);
            command.CommandName = input.CommandName;
            command.CommandJson = input.CommandJson;
            command.CommandState = input.CommandState;
            command.TaskId = input.TaskId;
            command.NodeId = input.NodeId;

        }

        public List<CommandListOutput> GetList(CommandListInput input)
        {
            var commands =
                _commandRepository.GetAll();
            if (!string.IsNullOrEmpty(input.CommandName))
                commands = commands.Where(a => a.CommandName == input.CommandName);
            commands = commands.OrderBy(a => a.Id).Skip(input.iDisplayStart).Take(input.iDisplayLength);
            return commands.MapTo<List<CommandListOutput>>();
        }

        public int GetListTotal(CommandListInput input)
        {
            var commands = _commandRepository.GetAll();
            if (!string.IsNullOrEmpty(input.CommandName))
                commands = commands.Where(a => a.CommandName == input.CommandName);
            return commands.Count();
        }
    }
}
