
using Abp.Dependency;
using TaskManager.Nodes;
using TaskManager.Nodes.Dto;

namespace TaskManager.HubService.SystemMonitor
{
    /// <summary>
    /// 节点心跳监控者
    /// 用于心跳通知数据库当前节点状态
    /// </summary>
    public class NodeHeartBeatMonitor : BaseMonitor
    {
        private readonly INodeAppService _nodeAppService;
        public override int Interval
        {
            get
            {
                return 5000;
            }
        }

        public NodeHeartBeatMonitor()
        {
            _nodeAppService = IocManager.Instance.Resolve<INodeAppService>();
        }

        protected override void Run()
        {
            // todo 更新并插入新的节点信息
            var node = _nodeAppService.GetNode(GlobalConfig.NodeId);
            if (node == null)
            {
                var input = new NodeInput
                {
                    NodeIp = System.Net.Dns.GetHostName(),
                    NodeName = "新增节点"
                };
                _nodeAppService.Create(input);
            }
            else
            {
                var input = new NodeInput
                {
                    Id = node.Id,
                    NodeIp = System.Net.Dns.GetHostName(),
                    NodeName = node.NodeName
                };
                _nodeAppService.Update(input);
            }
        }
    }
}
