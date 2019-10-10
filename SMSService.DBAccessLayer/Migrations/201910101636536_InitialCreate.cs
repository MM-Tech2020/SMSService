namespace SMSService.DBAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Applications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SiteName = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ApplicationSenders",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        AppId = c.Int(nullable: false),
                        SenderId = c.String(nullable: false, maxLength: 20),
                        FunctionCall = c.String(),
                        ProviderId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Providers", t => t.ProviderId)
                .ForeignKey("dbo.Applications", t => t.AppId)
                .Index(t => t.AppId)
                .Index(t => t.ProviderId);
            
            CreateTable(
                "dbo.OutGoingSMSBasicInfo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(nullable: false, maxLength: 1000),
                        language = c.String(nullable: false, maxLength: 15),
                        RequestDate = c.DateTime(nullable: false),
                        AppSenderId = c.Int(),
                        DelayUntil = c.String(maxLength: 50),
                        ResponseId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Responses", t => t.ResponseId)
                .ForeignKey("dbo.ApplicationSenders", t => t.AppSenderId)
                .Index(t => t.AppSenderId)
                .Index(t => t.ResponseId);
            
            CreateTable(
                "dbo.Responses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ResponseObject = c.String(),
                        StatusId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Status", t => t.StatusId)
                .Index(t => t.StatusId);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.String(nullable: false, maxLength: 100),
                        StatusCode = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SMSSenderNumbers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MNumberId = c.Int(),
                        SMSId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MobileNumbers", t => t.MNumberId)
                .ForeignKey("dbo.OutGoingSMSBasicInfo", t => t.SMSId)
                .Index(t => t.MNumberId)
                .Index(t => t.SMSId);
            
            CreateTable(
                "dbo.MobileNumbers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.String(nullable: false, maxLength: 15),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.InCommingSMS",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ResponseDateTime = c.DateTime(nullable: false),
                        MNumberId = c.Int(nullable: false),
                        MessageDesc = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MobileNumbers", t => t.MNumberId)
                .Index(t => t.MNumberId);
            
            CreateTable(
                "dbo.Providers",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Domain = c.String(nullable: false, maxLength: 100),
                        URI = c.String(maxLength: 100),
                        UserName = c.String(maxLength: 50),
                        Password = c.String(maxLength: 32),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplicationSenders", "AppId", "dbo.Applications");
            DropForeignKey("dbo.ApplicationSenders", "ProviderId", "dbo.Providers");
            DropForeignKey("dbo.OutGoingSMSBasicInfo", "AppSenderId", "dbo.ApplicationSenders");
            DropForeignKey("dbo.SMSSenderNumbers", "SMSId", "dbo.OutGoingSMSBasicInfo");
            DropForeignKey("dbo.SMSSenderNumbers", "MNumberId", "dbo.MobileNumbers");
            DropForeignKey("dbo.InCommingSMS", "MNumberId", "dbo.MobileNumbers");
            DropForeignKey("dbo.Responses", "StatusId", "dbo.Status");
            DropForeignKey("dbo.OutGoingSMSBasicInfo", "ResponseId", "dbo.Responses");
            DropIndex("dbo.InCommingSMS", new[] { "MNumberId" });
            DropIndex("dbo.SMSSenderNumbers", new[] { "SMSId" });
            DropIndex("dbo.SMSSenderNumbers", new[] { "MNumberId" });
            DropIndex("dbo.Responses", new[] { "StatusId" });
            DropIndex("dbo.OutGoingSMSBasicInfo", new[] { "ResponseId" });
            DropIndex("dbo.OutGoingSMSBasicInfo", new[] { "AppSenderId" });
            DropIndex("dbo.ApplicationSenders", new[] { "ProviderId" });
            DropIndex("dbo.ApplicationSenders", new[] { "AppId" });
            DropTable("dbo.Providers");
            DropTable("dbo.InCommingSMS");
            DropTable("dbo.MobileNumbers");
            DropTable("dbo.SMSSenderNumbers");
            DropTable("dbo.Status");
            DropTable("dbo.Responses");
            DropTable("dbo.OutGoingSMSBasicInfo");
            DropTable("dbo.ApplicationSenders");
            DropTable("dbo.Applications");
        }
    }
}
