namespace ZooApp.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AnimalQuantity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Animals", "AnimalQuantity", c => c.Int(nullable: false));
            DropColumn("dbo.Animals", "Quantity");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Animals", "Quantity", c => c.Int(nullable: false));
            DropColumn("dbo.Animals", "AnimalQuantity");
        }
    }
}
