namespace FileManager.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fileComments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FileComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CommentText = c.String(),
                        FileId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Files", t => t.FileId, cascadeDelete: true)
                .Index(t => t.FileId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FileComments", "FileId", "dbo.Files");
            DropIndex("dbo.FileComments", new[] { "FileId" });
            DropTable("dbo.FileComments");
        }
    }
}
