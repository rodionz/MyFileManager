namespace FileManager.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fileContent : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        FileId = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        FileContent = c.Binary(),
                        Comment = c.String(),
                        FolderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FileId)
                .ForeignKey("dbo.Folders", t => t.FolderId, cascadeDelete: true)
                .Index(t => t.FolderId);
            
            CreateTable(
                "dbo.Folders",
                c => new
                    {
                        FolderId = c.Int(nullable: false, identity: true),
                        FolderName = c.String(),
                    })
                .PrimaryKey(t => t.FolderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Files", "FolderId", "dbo.Folders");
            DropIndex("dbo.Files", new[] { "FolderId" });
            DropTable("dbo.Folders");
            DropTable("dbo.Files");
        }
    }
}
