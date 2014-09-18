namespace XlsModule
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using OfficeOpenXml;
    using System.IO;
    using OfficeOpenXml.Style;
    using ElecrtronicStoreSQLiteDB.Data;
    using ElectronicStoreMySQL.Data;

    public class XlsCreator
    {
        public static void GenerateExcelResult()
        {
            Console.WriteLine("Generating Excel report.....");
            var fileName = "Final Data" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + ".xlsx";

            var file = new FileInfo(fileName);

            using (var package = new ExcelPackage(file))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Results");
                worksheet.Row(1).Height = 20;
                worksheet.Row(2).Height = 18;

                worksheet.Cells[1, 1].Value = "Report";
                var headerRange = worksheet.Cells[1, 1, 1, 7];
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Font.Size = 16;
                headerRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.CenterContinuous;
                worksheet.Cells[2, 1].Value = "Product name";
                worksheet.Cells[2, 2].Value = "Store name";
                worksheet.Cells[2, 3].Value = "Quantity";
                worksheet.Cells[2, 4].Value = "Unit Price";
                worksheet.Cells[2, 5].Value = "Sum";
                worksheet.Cells[2, 6].Value = "Retail sum";
                worksheet.Cells[2, 7].Value = "profit";

                using (var range = worksheet.Cells[2, 1, 2, 7])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.Black);
                    range.Style.Font.Color.SetColor(Color.WhiteSmoke);
                    range.Style.ShrinkToFit = false;
                }

                var sqliteInfo = SQLiteManager.LoadAdditionalDataInfo();
                var reports = MySQLDataProvider.LoadReports();
                var rowNumber = 3;

                foreach (var report in reports)
                {
                    decimal profit = 0;
                    try
                    {
                        var foundItem = report.Sum * ((int)sqliteInfo.Where(sq => sq.InfoDescription == report.ProductName).FirstOrDefault().Mark)/100;

                        profit = foundItem;
                    }
                    catch (Exception)
                    {
                        profit = 0;
                    }

                    worksheet.Cells[rowNumber, 1].Value = report.ProductName;
                    worksheet.Cells[rowNumber, 2].Value = report.StoreName;
                    worksheet.Cells[rowNumber, 3].Value = report.Quantity;
                    worksheet.Cells[rowNumber, 4].Value = report.Price;
                    worksheet.Cells[rowNumber, 5].Value = report.Sum;
                    worksheet.Cells[rowNumber, 6].Value = report.Sum + profit;
                    worksheet.Cells[rowNumber, 7].Value = profit;

                    using (var rowRange = worksheet.Cells[rowNumber, 1, rowNumber, 7])
                    {
                        rowRange.Style.Font.Bold = false;
                        rowRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        rowRange.Style.Fill.BackgroundColor.SetColor(Color.LightSteelBlue);
                        rowRange.Style.Font.Color.SetColor(Color.Black);
                        rowRange.Style.ShrinkToFit = false;
                        rowRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        rowRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        rowRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        rowRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    }

                    rowNumber++;
                }
                worksheet.Column(1).AutoFit();
                worksheet.Column(2).AutoFit();
                worksheet.Column(3).AutoFit();
                worksheet.Column(4).AutoFit();
                worksheet.Column(5).AutoFit();
                worksheet.Column(6).AutoFit();
                worksheet.Column(7).AutoFit();

                package.Save();
                Console.WriteLine("Generating Excel report DONE!");
            }
        }
    }
}
