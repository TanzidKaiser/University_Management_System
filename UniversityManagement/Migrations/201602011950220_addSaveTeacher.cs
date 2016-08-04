namespace UniversityManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addSaveTeacher : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SaveTeachers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        Address = c.String(nullable: false, maxLength: 1000, unicode: false),
                        Email = c.String(nullable: false, maxLength: 100, unicode: false),
                        ContactNo = c.String(nullable: false, maxLength: 20, unicode: false),
                        DesignationId = c.Int(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                        CraditTaken = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .ForeignKey("dbo.Designations", t => t.DesignationId, cascadeDelete: true)
                .Index(t => t.Email, unique: true)
                .Index(t => t.DesignationId)
                .Index(t => t.DepartmentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SaveTeachers", "DesignationId", "dbo.Designations");
            DropForeignKey("dbo.SaveTeachers", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.SaveTeachers", new[] { "DepartmentId" });
            DropIndex("dbo.SaveTeachers", new[] { "DesignationId" });
            DropIndex("dbo.SaveTeachers", new[] { "Email" });
            DropTable("dbo.SaveTeachers");
        }
    }
}
