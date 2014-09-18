namespace XlsModule
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;
    using ElectronicStoresSystem.Data;
    using ElectronicStoresSystem.Models;

    public class XlsMigrator
    {
        public static void MigrateXslToSQL(ElectronicStoresSystemDbContext dbContex, List<List<Sale>> sales)
        {
            Console.WriteLine("Migrating stores from XML Xls to SQL server...");
            foreach (var collection in sales)
            {
                foreach (Sale sale in collection)
                {
                    if (!dbContex.Stores.Any(x => x.StoreName == sale.Store.StoreName))
                    {
                        dbContex.Stores.Add(sale.Store);
                        dbContex.SaveChanges();
                    }
                }
            }
            List<Sale> sll = new List<Sale>();
            foreach (var collection in sales)
            {

                foreach (Sale sale in collection)
                {
                    int storeId = dbContex.Stores.Where(s => s.StoreName == sale.Store.StoreName).FirstOrDefault().StoreId;
                    sale.StoreId = storeId;
                    sale.Store = null;
                    sll.Add(sale);
                    dbContex.Sales.Add(sale);
                }
            }

            Console.WriteLine();
            dbContex.SaveChanges();
        }
    }
}
