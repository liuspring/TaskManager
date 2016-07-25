using System.Data.Entity;
using Abp.EntityFramework;

namespace TaskManager.EntityFramework
{
     [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class SecondDbContext// : AbpDbContext
    {
        //public virtual IDbSet<Seconds.Second> Seconds { get; set; }

        //public SecondDbContext(): base("Second")
        //{

        //}
    }
}
