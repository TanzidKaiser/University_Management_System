namespace UniversityManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addStudentResult : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SaveStudentResults",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        Email = c.String(nullable: false, maxLength: 30, unicode: false),
                        Department = c.String(nullable: false, maxLength: 30, unicode: false),
                        CourseId = c.Int(nullable: false),
                        Grade = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: false)
                .ForeignKey("dbo.RegisterStudents", t => t.StudentId, cascadeDelete: false)
                .Index(t => t.StudentId)
                .Index(t => t.CourseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SaveStudentResults", "StudentId", "dbo.RegisterStudents");
            DropForeignKey("dbo.SaveStudentResults", "CourseId", "dbo.Courses");
            DropIndex("dbo.SaveStudentResults", new[] { "CourseId" });
            DropIndex("dbo.SaveStudentResults", new[] { "StudentId" });
            DropTable("dbo.SaveStudentResults");
        }
    }
}
