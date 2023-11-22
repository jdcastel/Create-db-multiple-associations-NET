namespace JDRA5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Actors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        AlternateName = c.String(maxLength: 150),
                        BirthDate = c.DateTime(),
                        Height = c.Double(),
                        ImageUrl = c.String(nullable: false, maxLength: 250),
                        Executive = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Shows",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        ReleaseDate = c.DateTime(nullable: false),
                        ImageUrl = c.String(nullable: false, maxLength: 250),
                        Coordinator = c.String(nullable: false, maxLength: 250),
                        Genre = c.String(nullable: false, maxLength: 50),
                        Genre_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Genres", t => t.Genre_Id)
                .Index(t => t.Genre_Id);
            
            CreateTable(
                "dbo.Episodes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        SeasonNumber = c.Int(nullable: false),
                        EpisodeNumber = c.Int(nullable: false),
                        Genre = c.String(nullable: false, maxLength: 50),
                        AirDate = c.DateTime(nullable: false),
                        ImageUrl = c.String(nullable: false, maxLength: 250),
                        Clerk = c.String(nullable: false, maxLength: 250),
                        ShowId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Shows", t => t.ShowId)
                .Index(t => t.ShowId);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ShowActors",
                c => new
                    {
                        Show_Id = c.Int(nullable: false),
                        Actor_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Show_Id, t.Actor_Id })
                .ForeignKey("dbo.Shows", t => t.Show_Id, cascadeDelete: true)
                .ForeignKey("dbo.Actors", t => t.Actor_Id, cascadeDelete: true)
                .Index(t => t.Show_Id)
                .Index(t => t.Actor_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Shows", "Genre_Id", "dbo.Genres");
            DropForeignKey("dbo.Episodes", "ShowId", "dbo.Shows");
            DropForeignKey("dbo.ShowActors", "Actor_Id", "dbo.Actors");
            DropForeignKey("dbo.ShowActors", "Show_Id", "dbo.Shows");
            DropIndex("dbo.ShowActors", new[] { "Actor_Id" });
            DropIndex("dbo.ShowActors", new[] { "Show_Id" });
            DropIndex("dbo.Episodes", new[] { "ShowId" });
            DropIndex("dbo.Shows", new[] { "Genre_Id" });
            DropTable("dbo.ShowActors");
            DropTable("dbo.Genres");
            DropTable("dbo.Episodes");
            DropTable("dbo.Shows");
            DropTable("dbo.Actors");
        }
    }
}
