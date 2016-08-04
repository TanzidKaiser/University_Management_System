namespace UniversityManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addRegNoinStudent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RegisterStudents", "RegNo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RegisterStudents", "RegNo");
        }
    }
}
