namespace DumpCRUDWinformApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreatee : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Students", "Class_Id", "dbo.Classes");
            DropIndex("dbo.Students", new[] { "Class_Id" });
            RenameColumn(table: "dbo.Students", name: "Class_Id", newName: "ClassId");
            AlterColumn("dbo.Students", "ClassId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Students", "ClassId");
            AddForeignKey("dbo.Students", "ClassId", "dbo.Classes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", "ClassId", "dbo.Classes");
            DropIndex("dbo.Students", new[] { "ClassId" });
            AlterColumn("dbo.Students", "ClassId", c => c.Guid());
            RenameColumn(table: "dbo.Students", name: "ClassId", newName: "Class_Id");
            CreateIndex("dbo.Students", "Class_Id");
            AddForeignKey("dbo.Students", "Class_Id", "dbo.Classes", "Id");
        }
    }
}
