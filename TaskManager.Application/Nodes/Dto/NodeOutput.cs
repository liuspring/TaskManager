using Abp.AutoMapper;

namespace TaskManager.Nodes.Dto
{
    [AutoMapFrom(typeof(Node))]
    public class NodeOutput
    {
        public int Id { get; set; }

        public string NodeName { get; set; }

        public string NodeIp { get; set; }
    }
}
