namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateData : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.qrtz_error", "node_id");
            CreateIndex("dbo.qrtz_error", "task_id");
            CreateIndex("dbo.qrtz_log", "node_id");
            CreateIndex("dbo.qrtz_log", "task_id");
            AddForeignKey("dbo.qrtz_error", "node_id", "dbo.qrtz_node", "id", cascadeDelete: true);
            AddForeignKey("dbo.qrtz_error", "task_id", "dbo.qrtz_task", "id", cascadeDelete: true);
            AddForeignKey("dbo.qrtz_log", "node_id", "dbo.qrtz_node", "id", cascadeDelete: true);
            AddForeignKey("dbo.qrtz_log", "task_id", "dbo.qrtz_task", "id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.qrtz_log", "task_id", "dbo.qrtz_task");
            DropForeignKey("dbo.qrtz_log", "node_id", "dbo.qrtz_node");
            DropForeignKey("dbo.qrtz_error", "task_id", "dbo.qrtz_task");
            DropForeignKey("dbo.qrtz_error", "node_id", "dbo.qrtz_node");
            DropIndex("dbo.qrtz_log", new[] { "task_id" });
            DropIndex("dbo.qrtz_log", new[] { "node_id" });
            DropIndex("dbo.qrtz_error", new[] { "task_id" });
            DropIndex("dbo.qrtz_error", new[] { "node_id" });
        }
    }
}
