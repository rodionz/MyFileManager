namespace FileManager.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fileNameChnagesisBack : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FileNameChanges",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OldName = c.String(nullable: false, maxLength: 30),
                        NewName = c.String(nullable: false, maxLength: 30),
                        FileChangeDate = c.DateTime(nullable: false),
                        FileId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Files", t => t.FileId, cascadeDelete: true)
                .Index(t => t.FileId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FileNameChanges", "FileId", "dbo.Files");
            DropIndex("dbo.FileNameChanges", new[] { "FileId" });
            DropTable("dbo.FileNameChanges");
        }
    }
}
