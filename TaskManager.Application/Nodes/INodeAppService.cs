using System.Collections.Generic;
using Abp.Application.Services;
using TaskManager.Nodes.Dto;

namespace TaskManager.Nodes
{
    public interface INodeAppService : IApplicationService
    {
        int Create(NodeInput input);

        NodeOutput GetNode(int id);

        void Update(NodeInput input);

        List<NodeListOutput> GetList(NodeListInput input);

        int GetListTotal(NodeListInput input);

        List<Node> GetAllList();

    }
}
