namespace ElecrtronicStoreSQLiteDB.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using ElecrtronicStoreSQLiteDB.Model;
    using ElectronicStoresSystem.Data;

    public static class SQLiteManager
    {
        private static Random rand = new Random();

        public static void SaveData(AdditionalData data)
        {
            using (var context = new ElectronicStoreSQLiteContext())
            {
                context.AdditionalDatas.Add(data);
                context.SaveChanges();
            }
        }

        public static IEnumerable<AdditionalData> LoadAdditionalDataInfo()
        {
            using (var context = new ElectronicStoreSQLiteContext())
            {
                return context.AdditionalDatas.ToList();
            }
        }

        public static void AddSqLiteData(ElectronicStoresSystemDbContext ctx)
        {
            var products = ctx.Products.ToList();

            foreach (var product in products)
            {
                var data = new AdditionalData
                {
                    InfoDescription = product.ProductName,
                    Mark = rand.Next(10, 20),
                };

                SaveData(data);
            }
        }
    }
}
