using Abp.AutoMapper;

namespace TaskManager.Logs.Dto
{
    [AutoMapFrom(typeof(Log))]
    public class LogOutput
    {
        public int Id { get; set; }
    }
}
