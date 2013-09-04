namespace DefectLog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initialize : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppVersions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VersionNumber = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Defects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DeveloperId = c.Int(),
                        TesterId = c.Int(),
                        StatusId = c.Int(),
                        CategoryId = c.Int(),
                        PriorityLevelId = c.Int(nullable: false),
                        AppVersionId = c.Int(nullable: false),
                        Summary = c.String(maxLength: 500),
                        Build = c.String(maxLength: 10),
                        Screen = c.String(maxLength: 30),
                        DateLogged = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.DeveloperId)
                .ForeignKey("dbo.Users", t => t.TesterId)
                .ForeignKey("dbo.Status", t => t.StatusId)
                .ForeignKey("dbo.AppVersions", t => t.AppVersionId, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.PriorityLevels", t => t.PriorityLevelId, cascadeDelete: true)
                .Index(t => t.DeveloperId)
                .Index(t => t.TesterId)
                .Index(t => t.StatusId)
                .Index(t => t.AppVersionId)
                .Index(t => t.CategoryId)
                .Index(t => t.PriorityLevelId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleId = c.Int(nullable: false),
                        UserName = c.String(maxLength: 50),
                        FirstName = c.String(maxLength: 50),
                        LastName = c.String(maxLength: 50),
                        EmailAddress = c.String(maxLength: 100),
                        Password = c.String(maxLength: 100),
                        PasswordSalt = c.String(maxLength: 100),
                        ResetPasswordKey = c.Guid(),
                        IsApproved = c.Boolean(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleName = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        DefectId = c.Int(nullable: false),
                        CommentText = c.String(maxLength: 500),
                        CommentDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Defects", t => t.DefectId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.DefectId);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 30),
                        CssClass = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PriorityLevels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PriorityName = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Comments", new[] { "DefectId" });
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.Defects", new[] { "PriorityLevelId" });
            DropIndex("dbo.Defects", new[] { "CategoryId" });
            DropIndex("dbo.Defects", new[] { "AppVersionId" });
            DropIndex("dbo.Defects", new[] { "StatusId" });
            DropIndex("dbo.Defects", new[] { "TesterId" });
            DropIndex("dbo.Defects", new[] { "DeveloperId" });
            DropForeignKey("dbo.Comments", "DefectId", "dbo.Defects");
            DropForeignKey("dbo.Comments", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Defects", "PriorityLevelId", "dbo.PriorityLevels");
            DropForeignKey("dbo.Defects", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Defects", "AppVersionId", "dbo.AppVersions");
            DropForeignKey("dbo.Defects", "StatusId", "dbo.Status");
            DropForeignKey("dbo.Defects", "TesterId", "dbo.Users");
            DropForeignKey("dbo.Defects", "DeveloperId", "dbo.Users");
            DropTable("dbo.PriorityLevels");
            DropTable("dbo.Categories");
            DropTable("dbo.Status");
            DropTable("dbo.Comments");
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
            DropTable("dbo.Defects");
            DropTable("dbo.AppVersions");
        }
    }
}
