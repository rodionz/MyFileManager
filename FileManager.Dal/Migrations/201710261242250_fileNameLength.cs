namespace FileManager.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fileNameLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FileNameChanges", "OldName", c => c.String(nullable: false));
            AlterColumn("dbo.FileNameChanges", "NewName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FileNameChanges", "NewName", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.FileNameChanges", "OldName", c => c.String(nullable: false, maxLength: 30));
        }
    }
}
