namespace JSONModule
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ElectronicStoreMySQL.Data;
    using ElectronicStoreMySQL.Model;
    using Newtonsoft.Json;
    using System.IO;

    public class JSONCreator
    {
        public static void CreateJSON(List<Report> reports)
        {
            Console.WriteLine("Generating JSON reports....");
            var reportsString = new List<string>();

            foreach (var report in reports)
            {
                string reportStr = JsonConvert.SerializeObject(report, Formatting.Indented);
                reportsString.Add(reportStr);
                File.WriteAllText(@"..\..\..\JSONReports\" + report.ReportId + ".json", reportStr);
            }
            Console.WriteLine("JSON reports generated!");
        }
    }
}
