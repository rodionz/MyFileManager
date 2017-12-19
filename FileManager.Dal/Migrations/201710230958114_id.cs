namespace FileManager.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class id : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FileChanges", "FileId", "dbo.Files");
            DropIndex("dbo.FileChanges", new[] { "FileId" });
            CreateTable(
                "dbo.FileHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileChangeText = c.String(),
                        FileChangeDate = c.DateTime(nullable: false),
                        FileId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Files", t => t.FileId, cascadeDelete: true)
                .Index(t => t.FileId);
            
            DropTable("dbo.FileChanges");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.FileChanges",
                c => new
                    {
                        FileChangeId = c.Int(nullable: false, identity: true),
                        FileChangeText = c.String(),
                        FileChangeDate = c.DateTime(nullable: false),
                        FileId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FileChangeId);
            
            DropForeignKey("dbo.FileHistories", "FileId", "dbo.Files");
            DropIndex("dbo.FileHistories", new[] { "FileId" });
            DropTable("dbo.FileHistories");
            CreateIndex("dbo.FileChanges", "FileId");
            AddForeignKey("dbo.FileChanges", "FileId", "dbo.Files", "FileId", cascadeDelete: true);
        }
    }
}
