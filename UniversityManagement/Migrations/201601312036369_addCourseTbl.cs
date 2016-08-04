namespace UniversityManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCourseTbl : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 20, unicode: false),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        Credit = c.Double(nullable: false),
                        Description = c.String(nullable: false, maxLength: 500, unicode: false),
                        DepatmentId = c.Int(nullable: false),
                        Semester = c.String(nullable: false, maxLength: 20, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DepatmentId, cascadeDelete: true)
                .Index(t => t.Code, unique: true)
                .Index(t => t.Name, unique: true)
                .Index(t => t.DepatmentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "DepatmentId", "dbo.Departments");
            DropIndex("dbo.Courses", new[] { "DepatmentId" });
            DropIndex("dbo.Courses", new[] { "Name" });
            DropIndex("dbo.Courses", new[] { "Code" });
            DropTable("dbo.Courses");
        }
    }
}
