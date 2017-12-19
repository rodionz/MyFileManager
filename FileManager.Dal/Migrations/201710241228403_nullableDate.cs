namespace FileManager.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullableDate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Files", "CreationDate", c => c.DateTime());
            AlterColumn("dbo.FileComments", "CommentDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FileComments", "CommentDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Files", "CreationDate", c => c.DateTime(nullable: false));
        }
    }
}
