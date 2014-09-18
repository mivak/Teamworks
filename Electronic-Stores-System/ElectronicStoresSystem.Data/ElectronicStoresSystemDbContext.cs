namespace ElectronicStoresSystem.Data
{
    using System.Data.Entity;
    using System.Data.SqlClient;
    using ElectronicStoresSystem.Data.Migrations;
    using ElectronicStoresSystem.Models;

    public class ElectronicStoresSystemDbContext: DbContext
    {
        public ElectronicStoresSystemDbContext()
            : base("ElectronicSystemConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ElectronicStoresSystemDbContext, Configuration>());
        }

        public IDbSet<Category> Categories { get; set; }

        public IDbSet<Manufacturer> Manufacturers { get; set; }

        public IDbSet<Product> Products { get; set; }

        public IDbSet<Expense> Expenses { get; set; }

        public IDbSet<Sale> Sales { get; set; }

        public IDbSet<Store> Stores { get; set; }
    }
}
