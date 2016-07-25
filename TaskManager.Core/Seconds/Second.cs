using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace TaskManager.Seconds
{
    [Table("second")]
    public class Second : Entity
    {
        public virtual string Name { get; set; }
    }
}
