using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace TaskManager.Logs.Dto
{
    [AutoMapFrom(typeof(Log))]
    public class LogInput : IInputDto
    {
        [Required]
        public int NodeId { get; set; }

        public int TaskId { get; set; }

        [Required]
        [StringLength(4000)]
        public string Msg { get; set; }

        [Required]
        public byte LogType { get; set; }
    }
}
