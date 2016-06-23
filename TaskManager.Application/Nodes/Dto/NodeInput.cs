using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace TaskManager.Nodes.Dto
{
    [AutoMapFrom(typeof(Node))]
    public class NodeInput : IInputDto
    {
        public const int MaxNodeNameLength = 50;
        public const int MaxNodeIpLength = 50;

        public int Id { get; set; }

        [Required]
        [StringLength(MaxNodeNameLength)]
        public string NodeName { get; set; }

        [Required]
        [StringLength(MaxNodeIpLength)]
        public string NodeIp { get; set; }
    }
}
