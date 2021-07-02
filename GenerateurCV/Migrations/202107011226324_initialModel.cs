namespace GenerateurCV.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CVs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Experiences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Poste = c.String(),
                        Description = c.String(),
                        Periode = c.String(),
                        CVid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CVs", t => t.CVid, cascadeDelete: true)
                .Index(t => t.CVid);
            
            CreateTable(
                "dbo.FormationDiplomes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Intitule = c.String(),
                        Date = c.DateTime(nullable: false),
                        CVid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CVs", t => t.CVid, cascadeDelete: true)
                .Index(t => t.CVid);
            
            CreateTable(
                "dbo.Loisirs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        CVid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CVs", t => t.CVid, cascadeDelete: true)
                .Index(t => t.CVid);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        Prenom = c.String(),
                        UserName = c.String(),
                        Password = c.String(),
                        Adresse = c.String(),
                        Telephone = c.Int(nullable: false),
                        Email = c.String(),
                        Photo = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CVs", "UserId", "dbo.Users");
            DropForeignKey("dbo.Loisirs", "CVid", "dbo.CVs");
            DropForeignKey("dbo.FormationDiplomes", "CVid", "dbo.CVs");
            DropForeignKey("dbo.Experiences", "CVid", "dbo.CVs");
            DropIndex("dbo.Loisirs", new[] { "CVid" });
            DropIndex("dbo.FormationDiplomes", new[] { "CVid" });
            DropIndex("dbo.Experiences", new[] { "CVid" });
            DropIndex("dbo.CVs", new[] { "UserId" });
            DropTable("dbo.Users");
            DropTable("dbo.Loisirs");
            DropTable("dbo.FormationDiplomes");
            DropTable("dbo.Experiences");
            DropTable("dbo.CVs");
        }
    }
}
