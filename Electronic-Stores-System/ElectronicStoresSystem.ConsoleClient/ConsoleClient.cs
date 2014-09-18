namespace ElectronicStoresSystem.ConsoleClient
{
    using System;
    using System.Linq;
    using ElecrtronicStoreSQLiteDB.Model;
    using ElectronicStoresSystem.Data;
    using MongoDB.Data;
    using MongoDb.Data;
    using MongoDb.Data.Entities;
    using ElectronicStoresSystem.Models;
    using ElecrtronicStoreSQLiteDB.Data;
    using ElectronicStoreMySQL.Model;
    using Telerik.OpenAccess;
    using ElectronicStoreMySQL.Data;
    using XlsModule;
    using XmlModule;
    using PDFModule;
    using JSONModule;

    class ConsoleClient
    {
        static void Main()
        {
            //Problem #1 – Load Excel Reports from ZIP File

            // Use once if your MongoDB is empty else delete your Mongo DATABASE for the project so it will generate it new every time
            MongoStartData.FillSampleCategories();
            MongoStartData.FillSampleManufacturers();
            MongoStartData.FillSampleProducts();

            // Use once if your SQL Database is empty else delete your SQL DATABASE for the project so it will generate
            // the data for the tables
            ElectronicStoresSystemDbContext dbContext = new ElectronicStoresSystemDbContext();

            MongoMigrator.MigrateMongoToSql(dbContext);

            using (dbContext)
            {
                XlsReader.ExtractZipReports();

                var sales = XlsReader.ReadAllExcells();
                XlsMigrator.MigrateXslToSQL(dbContext, sales);

                var expenses = XmlReader.GetXmlInfo();
                XmlMigrator.MigrateXmlToSQL(dbContext, expenses);

                MySqlInitializer.UpdateDatabase();
                MySqlReportsMigrator.MigrateReports(dbContext);

                SQLiteManager.AddSqLiteData(dbContext);
            }

            var reports = MySQLDataProvider.LoadReports();

            PDFCreator.CreatePDF(reports);
            XmlCreator.CreateXml(reports);
            JSONCreator.CreateJSON(reports);
            XlsCreator.GenerateExcelResult();

            Console.Write("Database update complete! Press any key to close.");
        }
    }
}
