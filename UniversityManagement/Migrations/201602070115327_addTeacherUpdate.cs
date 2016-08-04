namespace UniversityManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTeacherUpdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SaveTeachers", "CraditTaken", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SaveTeachers", "CraditTaken", c => c.Int(nullable: false));
        }
    }
}
