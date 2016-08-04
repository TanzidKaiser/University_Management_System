namespace UniversityManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addEnrollCourese : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EnrollCourses",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        Email = c.String(nullable: false, maxLength: 30, unicode: false),
                        Department = c.String(nullable: false, maxLength: 30, unicode: false),
                        CourseId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: false)
                .ForeignKey("dbo.RegisterStudents", t => t.StudentId, cascadeDelete: false)
                .Index(t => t.StudentId)
                .Index(t => t.CourseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EnrollCourses", "StudentId", "dbo.RegisterStudents");
            DropForeignKey("dbo.EnrollCourses", "CourseId", "dbo.Courses");
            DropIndex("dbo.EnrollCourses", new[] { "CourseId" });
            DropIndex("dbo.EnrollCourses", new[] { "StudentId" });
            DropTable("dbo.EnrollCourses");
        }
    }
}
