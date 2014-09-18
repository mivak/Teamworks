namespace MongoDb.Data
{
    using ElectronicStoresSystem.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
    using System.Threading.Tasks;
    using MongoDB.Data;
    using MongoDb.Data.Entities;

    public static class MongoMigrator
    {
        public static void MigrateMongoToSql(ElectronicStoresSystemDbContext dbContext)
        {
            Console.WriteLine("Starting migration of Mongo categories to SQL.");
            MigrateCategories(dbContext);
            Console.WriteLine("Migrating mongo categories to SQL done...");

            Console.WriteLine("Starting migration of Mongo manufacturers to SQL.");
            MigrateManufacturers(dbContext);
            Console.WriteLine("Migrating mongo manufacturers to SQL done...");

            Console.WriteLine("Starting migration of Mongo products to SQL.");
            MigrateProducts(dbContext);
            Console.WriteLine("Migrating mongo products to SQL done...");

            dbContext.SaveChanges();
        }

        private static void MigrateCategories(ElectronicStoresSystemDbContext dbContext)
        {
            var categories = MongoDbProvider.LoadData<MongoCategory>(MongoDbProvider.db);

            foreach (var category in categories)
            {
                dbContext.Categories.Add(MongoParser.ParseCategory(category));
            }
        }

        private static void MigrateManufacturers(ElectronicStoresSystemDbContext dbContext)
        {
            var manufacturers = MongoDbProvider.LoadData<MongoManufacturer>(MongoDbProvider.db);

            foreach (var manufacturer in manufacturers)
            {
                dbContext.Manufacturers.Add(MongoParser.ParseManufacturer(manufacturer));
            }
        }
        private static void MigrateProducts(ElectronicStoresSystemDbContext dbContext)
        {
            var products = MongoDbProvider.LoadData<MongoProduct>(MongoDbProvider.db);

            foreach (var product in products)
            {
                var newProduct = MongoParser.ParseProduct(product);
                if (newProduct.ProductName.Length > 99)
                {
                    Console.WriteLine("Cought!!!!");
                }
                dbContext.Products.Add(MongoParser.ParseProduct(product));
            }
        }
    }
}
