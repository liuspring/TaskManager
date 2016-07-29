using System.Collections.Generic;
using TaskManager.LoseYou;

namespace TaskManager.MigrationsLoseYou
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<TaskManager.EntityFramework.LoseYouDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"MigrationsLoseYou";
        }

        protected override void Seed(TaskManager.EntityFramework.LoseYouDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            var categories = new List<Category>
            {
                Category.Create("Í¼Æ¬", "pics", 1),
                Category.Create("ÊÓÆµ", "videos", 1),
                Category.Create("ÎÄÕª", "digests", 1)
            };
            context.Categories.AddOrUpdate(p=>p.CategoryCode,categories.ToArray());
        }
    }
}
