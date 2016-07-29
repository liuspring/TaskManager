namespace TaskManager.MigrationsLoseYou
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class InitCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ly_category",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        category_name = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        category_code = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        is_deleted = c.Boolean(nullable: false),
                        deleter_user_id = c.Long(),
                        deletion_time = c.DateTime(precision: 0),
                        last_modification_time = c.DateTime(precision: 0),
                        last_modifier_user_id = c.Long(),
                        creation_time = c.DateTime(nullable: false, precision: 0),
                        creator_user_id = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Category_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.ly_digests",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        main_id = c.Int(nullable: false),
                        content = c.String(nullable: false, unicode: false),
                        is_deleted = c.Boolean(nullable: false),
                        deleter_user_id = c.Long(),
                        deletion_time = c.DateTime(precision: 0),
                        last_modification_time = c.DateTime(precision: 0),
                        last_modifier_user_id = c.Long(),
                        creation_time = c.DateTime(nullable: false, precision: 0),
                        creator_user_id = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Digests_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.ly_main_info",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        main_name = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        content = c.String(maxLength: 500, storeType: "nvarchar"),
                        category_id = c.Int(nullable: false),
                        source_url = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        is_deleted = c.Boolean(nullable: false),
                        deleter_user_id = c.Long(),
                        deletion_time = c.DateTime(precision: 0),
                        last_modification_time = c.DateTime(precision: 0),
                        last_modifier_user_id = c.Long(),
                        creation_time = c.DateTime(nullable: false, precision: 0),
                        creator_user_id = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_MainInfo_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.ly_pics",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        main_id = c.Int(nullable: false),
                        title = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        current_path = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        original_url = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        is_deleted = c.Boolean(nullable: false),
                        deleter_user_id = c.Long(),
                        deletion_time = c.DateTime(precision: 0),
                        last_modification_time = c.DateTime(precision: 0),
                        last_modifier_user_id = c.Long(),
                        creation_time = c.DateTime(nullable: false, precision: 0),
                        creator_user_id = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Pics_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.ly_videos",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        main_id = c.Int(nullable: false),
                        video_current_path = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        video_original_url = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        pic_current_path = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        pic_original_url = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        is_deleted = c.Boolean(nullable: false),
                        deleter_user_id = c.Long(),
                        deletion_time = c.DateTime(precision: 0),
                        last_modification_time = c.DateTime(precision: 0),
                        last_modifier_user_id = c.Long(),
                        creation_time = c.DateTime(nullable: false, precision: 0),
                        creator_user_id = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Videos_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ly_videos",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Videos_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.ly_pics",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Pics_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.ly_main_info",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_MainInfo_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.ly_digests",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Digests_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.ly_category",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Category_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
