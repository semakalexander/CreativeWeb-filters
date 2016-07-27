namespace CreativeWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrationDataAnnot : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Items", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Items", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Items", "Type", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Items", "Type", c => c.String());
            AlterColumn("dbo.Items", "Description", c => c.String());
            AlterColumn("dbo.Items", "Name", c => c.String());
        }
    }
}
