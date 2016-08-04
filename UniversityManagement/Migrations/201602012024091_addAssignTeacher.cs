namespace UniversityManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addAssignTeacher : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CourseAssignTeachers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DepartmentId = c.Int(nullable: false),
                        TeacherId = c.Int(nullable: false),
                        CraditTaken = c.Double(nullable: false),
                        RemainCredit = c.Double(nullable: false),
                        CourseId = c.Int(nullable: false),
                        CoursName = c.String(nullable: false, maxLength: 20, unicode: false),
                        Credit = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: false)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: false)
                .ForeignKey("dbo.SaveTeachers", t => t.TeacherId, cascadeDelete: false)
                .Index(t => t.DepartmentId)
                .Index(t => t.TeacherId)
                .Index(t => t.CourseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CourseAssignTeachers", "TeacherId", "dbo.SaveTeachers");
            DropForeignKey("dbo.CourseAssignTeachers", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.CourseAssignTeachers", "CourseId", "dbo.Courses");
            DropIndex("dbo.CourseAssignTeachers", new[] { "CourseId" });
            DropIndex("dbo.CourseAssignTeachers", new[] { "TeacherId" });
            DropIndex("dbo.CourseAssignTeachers", new[] { "DepartmentId" });
            DropTable("dbo.CourseAssignTeachers");
        }
    }
}
