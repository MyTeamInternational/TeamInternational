namespace TeamProject.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingImagePath : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "ImagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movies", "ImagePath");
        }
    }
}
