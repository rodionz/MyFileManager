namespace FileManager.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class commentsAndChanges : DbMigration
    {
        public override void Up()
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
                .PrimaryKey(t => t.FileChangeId)
                .ForeignKey("dbo.Files", t => t.FileId, cascadeDelete: true)
                .Index(t => t.FileId);
            
            CreateTable(
                "dbo.FileComments",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        FileId = c.Int(nullable: false),
                        CommentDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.Files", t => t.FileId, cascadeDelete: true)
                .Index(t => t.FileId);
            
            AddColumn("dbo.Files", "FilePath", c => c.String());
            AddColumn("dbo.Files", "CreationDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Files", "Comment");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Files", "Comment", c => c.String());
            DropForeignKey("dbo.FileChanges", "FileId", "dbo.Files");
            DropForeignKey("dbo.FileComments", "FileId", "dbo.Files");
            DropIndex("dbo.FileComments", new[] { "FileId" });
            DropIndex("dbo.FileChanges", new[] { "FileId" });
            DropColumn("dbo.Files", "CreationDate");
            DropColumn("dbo.Files", "FilePath");
            DropTable("dbo.FileComments");
            DropTable("dbo.FileChanges");
        }
    }
}
