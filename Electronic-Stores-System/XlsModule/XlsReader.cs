namespace XlsModule
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Ionic.Zip;
    using ElectronicStoresSystem.Models;
    using System.Data.OleDb;
    using System.Data;
    using System.Globalization;

    public class XlsReader
    {
        public static void ExtractZipReports()
        {
            string zipToUnpack = @"..\..\..\Sales-report.zip";
            string unpackDirectory = @"..\..\Extracted Files";

            using (ZipFile zip = ZipFile.Read(zipToUnpack))
            {
                foreach (ZipEntry e in zip)
                {
                    e.Extract(unpackDirectory, ExtractExistingFileAction.OverwriteSilently);
                }
            }
        }

        public static List<List<Sale>> ReadAllExcells()
        {
            Console.WriteLine("Parsing Sales from excel files....");
            List<List<Sale>> data = new List<List<Sale>>();
            string firstConStringPart = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=";
            string secondConStringPart = @";Extended Properties='Excel 12.0 Xml;HDR=YES'";
            //IF time DFS
            string[] fileNames = { @"..\..\Extracted Files\20-Aug-2014\Bourgas-Technomarket-Sales-Report-20-Aug-2013.xls",
                                     @"..\..\Extracted Files\20-Aug-2014\Plovdiv-Supermarket-Billa-Sales-Report-20-Aug-2013.xls",
                                     @"..\..\Extracted Files\20-Aug-2014\Sofia-Supermarket-Metro-Sales-Report-20-Aug-2013.xls",
                                    @"..\..\Extracted Files\21-Aug-2014\Bourgas-Technomarket-Sales-Report-21-Aug-2013.xls",
                                    @"..\..\Extracted Files\21-Aug-2014\Plovdiv-Supermarket-Billa-Sales-Report-21-Aug-2013.xls",
                                    @"..\..\Extracted Files\21-Aug-2014\Sofia-Supermarket-Metro-Sales-Report-21-Aug-2013.xls",
                                     @"..\..\Extracted Files\22-Aug-2014\Bourgas-Technomarket-Sales-Report-22-Aug-2013.xls",
                                      @"..\..\Extracted Files\22-Aug-2014\Plovdiv-Supermarket-Billa-Sales-Report-22-Aug-2013.xls",
                                     @"..\..\Extracted Files\22-Aug-2014\Sofia-Supermarket-Billa-Sales-Report-22-Aug-2013.xls",
                                     @"..\..\Extracted Files\22-Aug-2014\Sofia-Supermarket-Metro-Sales-Report-22-Aug-2013.xls",

                                    };
            //Console.WriteLine(firstConStringPart + fileNames[0] + secondConStringPart);


            foreach (var path in fileNames)
            {
                data.Add(ReadCurrentExcel(firstConStringPart + path + secondConStringPart));
            }

            Console.WriteLine("Parsing of Sales successful!");
            return data;
        }

        private static List<Sale> ReadCurrentExcel(string path)
        {
            List<Sale> allSales = new List<Sale>();
            using (OleDbConnection conn = new OleDbConnection(path))
            {

                conn.Open();
                string command = @"select * from [Sales$]";
                OleDbDataAdapter adapter = new OleDbDataAdapter(command, conn);
                DataTable table = new DataTable();
                using (adapter)
                {
                    adapter.FillSchema(table, SchemaType.Source);
                    adapter.Fill(table);
                }

                int counter = 0;
                string[] splittedName = path.Split(new string[] { "-Sales-Report-" }, StringSplitOptions.RemoveEmptyEntries);
                string location = splittedName[0].Substring(splittedName[0].LastIndexOf('\\') + 1).Replace(@"-", " ");
                int dotIndex = splittedName[1].IndexOf('.');
                string date = splittedName[1].Substring(0, dotIndex);
                foreach (DataRow row in table.Rows)
                {
                    Sale currentSale = new Sale();
                    List<decimal> info = new List<decimal>();
                    bool getInside = false;

                    foreach (DataColumn col in table.Columns)
                    {
                        var count = table.Columns.Count;
                        if (row[col].ToString() != "")
                        {
                            decimal number = 0;
                            bool success = decimal.TryParse(row[col].ToString(), out number);
                            if (success)
                            {
                                info.Add(number);
                                getInside = true;
                            }
                            else
                            {
                                break;
                            }
                        }

                    }

                    if (getInside) //&& counter != table.Rows.Count - 1)
                    {
                        currentSale.ProductId = (int)info[0];
                        currentSale.Quantity = (int)info[1];
                        currentSale.Price = info[2];
                        currentSale.Sum = info[3];
                        currentSale.Store = new Store { StoreName = location };
                        currentSale.Date = DateTime.ParseExact(date, "dd-MMM-yyyy", CultureInfo.InvariantCulture);
                        allSales.Add(currentSale);
                    }
                    //counter++;
                }

            }

            return allSales;
        }
    }
}