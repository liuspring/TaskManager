using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace TaskManager.Categories.Dto
{
    [AutoMapFrom(typeof(Category))]
    public class CategoryInput : IInputDto
    {
        public const int MaxCategoryNameLength = 50;

        public int Id { get; set; }

        [Required]
        [StringLength(MaxCategoryNameLength)]
        public string CategoryName { get; set; }

    }
}
