namespace FileManager.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _fixed : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.FileHistories", newName: "FileNameChanges");
            DropForeignKey("dbo.FileComments", "FileId", "dbo.Files");
            DropIndex("dbo.FileComments", new[] { "FileId" });
            DropTable("dbo.FileComments");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.FileComments",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        FileId = c.Int(nullable: false),
                        CommentDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.CommentId);
            
            CreateIndex("dbo.FileComments", "FileId");
            AddForeignKey("dbo.FileComments", "FileId", "dbo.Files", "FileId", cascadeDelete: true);
            RenameTable(name: "dbo.FileNameChanges", newName: "FileHistories");
        }
    }
}
