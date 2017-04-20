namespace TeamProject.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Views", "MovieID", "dbo.Movies");
            DropForeignKey("dbo.Views", "UserID", "dbo.Users");
            DropIndex("dbo.Views", new[] { "UserID" });
            DropIndex("dbo.Views", new[] { "MovieID" });
            CreateTable(
                "dbo.Pictures",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MovieID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Movies", t => t.MovieID, cascadeDelete: true)
                .Index(t => t.MovieID);
            
            DropColumn("dbo.Movies", "ImagePath");
            DropTable("dbo.Views");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Views",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Time = c.Time(nullable: false, precision: 7),
                        UserID = c.Int(nullable: false),
                        MovieID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Movies", "ImagePath", c => c.String());
            DropForeignKey("dbo.Pictures", "MovieID", "dbo.Movies");
            DropIndex("dbo.Pictures", new[] { "MovieID" });
            DropTable("dbo.Pictures");
            CreateIndex("dbo.Views", "MovieID");
            CreateIndex("dbo.Views", "UserID");
            AddForeignKey("dbo.Views", "UserID", "dbo.Users", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Views", "MovieID", "dbo.Movies", "ID", cascadeDelete: true);
        }
    }
}
