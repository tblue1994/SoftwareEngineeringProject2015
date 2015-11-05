namespace Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Activities",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AccountId = c.String(nullable: false, maxLength: 128),
                        Type = c.Int(nullable: false),
                        Description = c.String(nullable: false),
                        Steps = c.Int(nullable: false),
                        Distance = c.Double(nullable: false),
                        BeginTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AccountId, cascadeDelete: true)
                .Index(t => t.AccountId);
            
            CreateTable(
                "dbo.Attainments",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AccountId = c.String(nullable: false, maxLength: 128),
                        BadgeId = c.Long(nullable: false),
                        DateEarned = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AccountId, cascadeDelete: true)
                .Index(t => t.AccountId);
            
            CreateTable(
                "dbo.Badges",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Timeline = c.Int(nullable: false),
                        TargetId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Targets", t => t.TargetId, cascadeDelete: true)
                .Index(t => t.TargetId);
            
            CreateTable(
                "dbo.Targets",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        TargetNumber = c.Double(nullable: false),
                        ActivityType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Goals",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Timeline = c.Int(nullable: false),
                        BeginDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        TargetId = c.Long(nullable: false),
                        Progress = c.Double(nullable: false),
                        AccountId = c.String(nullable: false, maxLength: 128),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Targets", t => t.TargetId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.AccountId, cascadeDelete: true)
                .Index(t => t.TargetId)
                .Index(t => t.AccountId);
            
            CreateTable(
                "dbo.Memberships",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AccountId = c.String(nullable: false, maxLength: 128),
                        TeamId = c.Long(nullable: false),
                        Status = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.AccountId, cascadeDelete: true)
                .Index(t => t.AccountId)
                .Index(t => t.TeamId);
            
            CreateTable(
                "dbo.Moods",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Paths",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ActivityId = c.Long(nullable: false),
                        Data = c.Binary(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Reports",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AccountId = c.String(nullable: false, maxLength: 128),
                        Steps = c.Int(nullable: false),
                        Distance = c.Double(nullable: false),
                        Duration = c.Double(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AccountId, cascadeDelete: true)
                .Index(t => t.AccountId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AccountId = c.String(nullable: false, maxLength: 128),
                        MoodId = c.Long(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AccountId, cascadeDelete: true)
                .Index(t => t.AccountId);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FullName = c.String(nullable: false),
                        PreferredName = c.String(nullable: false),
                        Zip = c.Int(nullable: false),
                        FacebookId = c.Long(),
                        TwitterId = c.Long(),
                        Birthdate = c.DateTime(nullable: false),
                        Sex = c.Boolean(),
                        Weight = c.Int(nullable: false),
                        Height = c.Int(nullable: false),
                        PictureUrl = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Foods",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AccountId = c.String(nullable: false, maxLength: 128),
                        FoodName = c.String(nullable: false),
                        Amount = c.Double(nullable: false),
                        Measurement = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AccountId, cascadeDelete: true)
                .Index(t => t.AccountId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Workouts",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AccountId = c.String(nullable: false, maxLength: 128),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AccountId, cascadeDelete: true)
                .Index(t => t.AccountId);
            
            CreateTable(
                "dbo.Exercises",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        WorkoutId = c.Long(nullable: false),
                        Description = c.String(nullable: false),
                        Sets = c.Int(nullable: false),
                        Repetitions = c.Int(nullable: false),
                        Weight = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Workouts", t => t.WorkoutId, cascadeDelete: true)
                .Index(t => t.WorkoutId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Workouts", "AccountId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Exercises", "WorkoutId", "dbo.Workouts");
            DropForeignKey("dbo.Status", "AccountId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Reports", "AccountId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Memberships", "AccountId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Goals", "AccountId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Foods", "AccountId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Attainments", "AccountId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Activities", "AccountId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Memberships", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Goals", "TargetId", "dbo.Targets");
            DropForeignKey("dbo.Badges", "TargetId", "dbo.Targets");
            DropIndex("dbo.Exercises", new[] { "WorkoutId" });
            DropIndex("dbo.Workouts", new[] { "AccountId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.Foods", new[] { "AccountId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Status", new[] { "AccountId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Reports", new[] { "AccountId" });
            DropIndex("dbo.Memberships", new[] { "TeamId" });
            DropIndex("dbo.Memberships", new[] { "AccountId" });
            DropIndex("dbo.Goals", new[] { "AccountId" });
            DropIndex("dbo.Goals", new[] { "TargetId" });
            DropIndex("dbo.Badges", new[] { "TargetId" });
            DropIndex("dbo.Attainments", new[] { "AccountId" });
            DropIndex("dbo.Activities", new[] { "AccountId" });
            DropTable("dbo.Exercises");
            DropTable("dbo.Workouts");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.Foods");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Teams");
            DropTable("dbo.Status");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Reports");
            DropTable("dbo.Paths");
            DropTable("dbo.Moods");
            DropTable("dbo.Memberships");
            DropTable("dbo.Goals");
            DropTable("dbo.Targets");
            DropTable("dbo.Badges");
            DropTable("dbo.Attainments");
            DropTable("dbo.Activities");
        }
    }
}
