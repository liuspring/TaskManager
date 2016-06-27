
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace TaskManager.TempDatas.Dto
{
    [AutoMapFrom(typeof(TempData))]
    public class TempDataInput : IInputDto
    {
        public int TaskId { get; set; }

        [StringLength(50)]
        public string DataJson { get; set; }
    }
}
