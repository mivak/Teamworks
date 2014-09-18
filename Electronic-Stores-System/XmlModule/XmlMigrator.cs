namespace XmlModule
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;
    using ElectronicStoresSystem.Data;
    using ElectronicStoresSystem.Models;

    public class XmlMigrator
    {
        public static void MigrateXmlToSQL(ElectronicStoresSystemDbContext dbContex, IList<Expense> expenses)
        {
            Console.WriteLine("Migrating expenses from XML to SQL server...");
            foreach (var expense in expenses)
            {
                int manufacturerId = dbContex.Manufacturers.Where(m => m.ManufacturerName == expense.Manufacturer.ManufacturerName).FirstOrDefault().ManufacturerId;
                expense.ManufacturerId = manufacturerId;
                expense.Manufacturer = null;
                dbContex.Expenses.Add(expense);
            }

            dbContex.SaveChanges();
            Console.WriteLine("XML expenses to SQL migrated successfully");
        }
    }
}