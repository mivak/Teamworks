namespace ElectronicStoreMySQL.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ElectronicStoreMySQL.Model;

    public static class MySQLDataProvider
    {
        public static void SaveReport(Report report)
        {
            var ctx = new ElectronicStoreMySQLFluentModel();

            using (ctx)
            {
                ctx.Add(report);
                ctx.SaveChanges();
            }
        }

        public static List<Report> LoadReports()
        {
            var ctx = new ElectronicStoreMySQLFluentModel();

            using (ctx)
            {
                return ctx.Reports.ToList();
            }
        }
    }
}
