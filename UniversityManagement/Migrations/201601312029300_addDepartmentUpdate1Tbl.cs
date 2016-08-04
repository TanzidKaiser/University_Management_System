namespace UniversityManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDepartmentUpdate1Tbl : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Departments", "Code", unique: true);
            CreateIndex("dbo.Departments", "Name", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Departments", new[] { "Name" });
            DropIndex("dbo.Departments", new[] { "Code" });
        }
    }
}
