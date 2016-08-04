namespace UniversityManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addRegisterStudent : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RegisterStudents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        Email = c.String(nullable: false, maxLength: 100, unicode: false),
                        ContactNo = c.String(nullable: false, maxLength: 20, unicode: false),
                        date = c.DateTime(nullable: false),
                        Address = c.String(nullable: false, maxLength: 500, unicode: false),
                        DepartmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .Index(t => t.Email, unique: true)
                .Index(t => t.DepartmentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RegisterStudents", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.RegisterStudents", new[] { "DepartmentId" });
            DropIndex("dbo.RegisterStudents", new[] { "Email" });
            DropTable("dbo.RegisterStudents");
        }
    }
}
