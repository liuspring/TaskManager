using System.Collections.Generic;
using System.Linq;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using TaskManager.Authorization;
using TaskManager.Nodes.Dto;

namespace TaskManager.Nodes
{
    public class NodeAppService : TaskManagerAppServiceBase, INodeAppService
    {
        private readonly IRepository<Node, int> _nodeRepository;

        public NodeAppService(IRepository<Node, int> nodeRepository)
        {
            _nodeRepository = nodeRepository;
        }

        public int Create(NodeInput input)
        {
            var node = Node.Create(input.NodeName, input.NodeIp);
            _nodeRepository.Insert(node);
            return node.Id;
        }

        public NodeOutput GetNode(int id)
        {
            var node = _nodeRepository.Get(id);
            return node.MapTo<NodeOutput>();
        }

        public void Update(NodeInput input)
        {
            var node = _nodeRepository.Get(input.Id);
            node.NodeName = input.NodeName;
            node.NodeIp = input.NodeIp;
        }

        public List<NodeListOutput> GetList(NodeListInput input)
        {
            var nodes = _nodeRepository.GetAll()
                .Where(a => a.NodeName.Contains(input.NodeName.Trim()))
                .OrderBy(a => a.Id)
                .Skip(input.iDisplayStart)
                .Take(input.iDisplayLength);
            return nodes.MapTo<List<NodeListOutput>>();

        }

        public int GetListTotal(NodeListInput input)
        {
            return _nodeRepository.GetAll().Count(a => a.NodeName.Contains(input.NodeName.Trim()));
        }

        public List<Node> GetAllList()
        {
            return _nodeRepository.GetAllList();
        }
    }
}
