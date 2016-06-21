﻿using System.Data.Common;
using System.Data.Entity;
using Abp.Zero.EntityFramework;
using TaskManager.Authorization.Roles;
using TaskManager.MultiTenancy;
using TaskManager.Users;

namespace TaskManager.EntityFramework
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class TaskManagerDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
        //TODO: Define an IDbSet for your Entities...
        public virtual IDbSet<Categories.Category> Categories { get; set; }

        public virtual IDbSet<Nodes.Node> Nodes { get; set; }

        public virtual IDbSet<Tasks.Task> Tasks { get; set; }

        public virtual IDbSet<Commands.Command> Commands { get; set; }

        public virtual IDbSet<TempDatas.TempData> TempDatas { get; set; }

        public virtual IDbSet<Versions.VersionInfo> Versions { get; set; }

        public virtual IDbSet<Performances.Performance> Performances { get; set; }

        public virtual IDbSet<Errors.Error> Errors { get; set; }

        public virtual IDbSet<Logs.Log> Logs { get; set; }
        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public TaskManagerDbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in TaskManagerDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of TaskManagerDbContext since ABP automatically handles it.
         */
        public TaskManagerDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        //This constructor is used in tests
        public TaskManagerDbContext(DbConnection connection)
            : base(connection, true)
        {

        }
    }
}
