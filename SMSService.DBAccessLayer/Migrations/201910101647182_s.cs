namespace SMSService.DBAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class s : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ApplicationSenders", "FunctionCall", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ApplicationSenders", "FunctionCall", c => c.String());
        }
    }
}
