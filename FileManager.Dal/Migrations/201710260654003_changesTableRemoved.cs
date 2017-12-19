namespace FileManager.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changesTableRemoved : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FileNameChanges", "FileId", "dbo.Files");
            DropIndex("dbo.FileNameChanges", new[] { "FileId" });
            AlterColumn("dbo.Files", "FileName", c => c.String(nullable: false));
            AlterColumn("dbo.Folders", "FolderName", c => c.String(nullable: false, maxLength: 30));
            DropTable("dbo.FileNameChanges");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.FileNameChanges",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OldName = c.String(),
                        NewName = c.String(),
                        FileChangeDate = c.DateTime(nullable: false),
                        FileId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.Folders", "FolderName", c => c.String());
            AlterColumn("dbo.Files", "FileName", c => c.String());
            CreateIndex("dbo.FileNameChanges", "FileId");
            AddForeignKey("dbo.FileNameChanges", "FileId", "dbo.Files", "FileId", cascadeDelete: true);
        }
    }
}
