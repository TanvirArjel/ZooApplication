namespace ZooApp.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UniqueName : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Animals", "Ix_AnimalName");
            DropIndex("dbo.Foods", "Ix_FoodName");
            CreateIndex("dbo.Animals", "AnimalName", unique: true, name: "Ix_AnimalName");
            CreateIndex("dbo.Foods", "FoodName", unique: true, name: "Ix_FoodName");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Foods", "Ix_FoodName");
            DropIndex("dbo.Animals", "Ix_AnimalName");
            CreateIndex("dbo.Foods", "FoodName", name: "Ix_FoodName");
            CreateIndex("dbo.Animals", "AnimalName", name: "Ix_AnimalName");
        }
    }
}
