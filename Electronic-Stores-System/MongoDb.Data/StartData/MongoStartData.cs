namespace MongoDb.Data
{
    using System;
    using System.Linq;
    using MongoDB.Data;
    using MongoDb.Data.Entities;
    using ElectronicStoresSystem.Data;

    public static class MongoStartData
    {
        private static Random rand = new Random();

        public static void FillSampleManufacturers()
        {
            var manufacturers = new string[] { "Samsung Electronics Co.", "Nokia", "Apple Inc.", "LG Electronics", 
                                                "ZTE", "Huawei", "TCL Communication", "Lenovo",
                                                "Sony Mobile Communications", "Panasonic Corp." };

            for (int i = 0; i < manufacturers.Length; i++)
            {
                var manufacturer = new MongoManufacturer
                {
                    ManufacturerId = i,
                    ManufacturerName = manufacturers[i],
                };

                MongoDbProvider.SaveData<MongoManufacturer>(MongoDbProvider.db, manufacturer);
            }
        }

        public static void FillSampleCategories()
        {
            var categories = new string[] { "Smartphone", "Laptop", "Tablet", "PersonalComputer", "Oven", 
                                            "LED Tellevisor", "Microwave", "Toaster", "Printer", 
                                            "Vibrator", "Vacuum cleaner", "Boiler" };

            for (int i = 0; i < categories.Length; i++)
            {
                var category = new MongoCategory
                {
                    CategoryId = i,
                    CategoryName = categories[i],
                };

                MongoDbProvider.SaveData<MongoCategory>(MongoDbProvider.db, category);
            }
        }

        public static void FillSampleProducts()
        {
            var manufacturers = MongoDbProvider.LoadData<MongoManufacturer>(MongoDbProvider.db);
            var categories = MongoDbProvider.LoadData<MongoCategory>(MongoDbProvider.db);
            var products = new string[] { "S6000", "A3000", "P430", "V210", "E320", "W210", "OW435", "K9800", "M1000",
                "M200", "N200", "S3000", "F400", "L450", "B2000", "A300", "J450", "C3500", "JS2100", "A2100", "D5050",
                "G4580", "V340", "R3500", "T680", "B2450", "Z210", "D3200", "Y1220", "X2400", "Y9000", "T500", "R450"};

            for (int i = 0; i < 100; i++)
            {
                var categoryId = rand.Next(1, categories.Count());
                var manufacturerId = rand.Next(1, manufacturers.Count());

                var product = new MongoProduct
                {
                    BasePrice = (rand.Next(1, 100) + categoryId + manufacturerId),
                    CategoryId = categoryId,
                    ManufacturerId = manufacturerId,
                    ProductId = i + 1,
                    ProductName = categories.ToList()[categoryId].CategoryName + " " + products[rand.Next(0, products.Length)],
                };

                MongoDbProvider.SaveData<MongoProduct>(MongoDbProvider.db, product);
            }
        }
    }
}
