using System.Data.Entity;
using Abp.EntityFramework;
using TaskManager.LoseYou;

namespace TaskManager.EntityFramework
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class LoseYouDbContext : AbpDbContext
    {
        public virtual IDbSet<Category> Categories { get; set; }
        public virtual IDbSet<MainInfo> MainInfos { get; set; }
        public virtual IDbSet<Pics> Pics { get; set; }
        public virtual IDbSet<Videos> Videos { get; set; }
        public virtual IDbSet<Digests> Digests { get; set; }

        public LoseYouDbContext()
            : base("LoseYou")
        {

        }

    }
}
