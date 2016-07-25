namespace TaskManager.SecondDbContext
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration //: DbMigrationsConfiguration<TaskManager.EntityFramework.SecondDbContext>
    {
        //public Configuration()
        //{
        //    AutomaticMigrationsEnabled = false;
        //    MigrationsDirectory = @"SecondDbContext";
        //}

        //protected override void Seed(TaskManager.EntityFramework.SecondDbContext context)
        //{
        //    //  This method will be called after migrating to the latest version.

        //    //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
        //    //  to avoid creating duplicate seed data. E.g.
        //    //
        //    //    context.People.AddOrUpdate(
        //    //      p => p.FullName,
        //    //      new Person { FullName = "Andrew Peters" },
        //    //      new Person { FullName = "Brice Lambson" },
        //    //      new Person { FullName = "Rowan Miller" }
        //    //    );
        //    //
        //}
    }
}
