namespace ElectronicStoreMySQL.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Telerik.OpenAccess;

    using ElectronicStoreMySQL.Model;
    using ElectronicStoresSystem.Data;

    public static class MySqlReportsMigrator
    {
        public static void MigrateReports(ElectronicStoresSystemDbContext storeContext)
        {
            var mySqlContext = new ElectronicStoreMySQLFluentModel();

            var reports = storeContext.Sales.OrderBy(s => s.Store.StoreName).ToList();

            for (int i = 1, len = reports.Count; i < len; i++)
            {
                var newReport = new Report
                {
                    ReportId = i,
                    Price = reports[i].Price,
                    ProductName = reports[i].Product.ProductName,
                    Quantity = reports[i].Quantity,
                    StoreName = reports[i].Store.StoreName,
                    Sum = reports[i].Sum,
                };

                using (var ctx = new ElectronicStoreMySQLFluentModel())
                {
                    ctx.Add(newReport);
                    ctx.SaveChanges();
                }
            }
        }
    }
}
