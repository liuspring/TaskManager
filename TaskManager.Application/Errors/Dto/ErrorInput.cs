using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;

namespace TaskManager.Errors.Dto
{
    [AutoMapFrom(typeof(Error))]
    public class ErrorInput
    {
        [Required]
        public int NodeId { get; set; }

        public int TaskId { get; set; }

        [Required]
        [StringLength(4000)]
        public string Msg { get; set; }

        [Required]
        public byte ErrorType { get; set; }
    }
}
