namespace GeneralStoreAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedProductClas : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "Name", c => c.String(nullable: false));
            DropColumn("dbo.Products", "IsInStock");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "IsInStock", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Products", "Name", c => c.String());
        }
    }
}
