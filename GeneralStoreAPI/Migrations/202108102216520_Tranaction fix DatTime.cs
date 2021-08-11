namespace GeneralStoreAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TranactionfixDatTime : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Transactions", "DateOfTransaction");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Transactions", "DateOfTransaction", c => c.DateTime(nullable: false));
        }
    }
}
