using Abp.AutoMapper;

namespace TaskManager.Errors.Dto
{
    [AutoMapFrom(typeof(Error))]
    public class ErrorOutput
    {
        public int Id { get; set; }
    }
}
