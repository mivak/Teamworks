namespace ElectronicStoresSystem.Data.Migrations
{
    using ElectronicStoresSystem.Models;
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using ElectronicStoresSystem.Data;

    public sealed class Configuration : DbMigrationsConfiguration<ElectronicStoresSystemDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "ElectronicStoresSystem.Data.ElectronicStoresSystemDbContext";
        }

        protected override void Seed(ElectronicStoresSystemDbContext context)
        {
        }
    }
}
