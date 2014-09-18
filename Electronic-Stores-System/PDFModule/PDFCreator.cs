namespace PDFModule
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using iTextSharp.text;
    using iTextSharp.text.pdf;
    using System.IO;
    using ElectronicStoreMySQL.Model;
    using ElectronicStoresSystem.Data;
    using ElectronicStoresSystem.Models;
    using Telerik.OpenAccess;
    using System.Data.Entity;

    public class PDFCreator
    {
        public static void CreatePDF(List<Report> reports)
        {
                
            Document myDocument = new Document(PageSize.A4.Rotate());

            try
            {
                PdfWriter.GetInstance(myDocument, new FileStream(@"..\..\..\salesReport.pdf", FileMode.Create));
                PdfPTable table = new PdfPTable(5);
                float[] widths = new float[] { 25f, 10f, 10f, 45f, 10f };
                table.SetWidths(widths);
                table.DefaultCell.FixedHeight = 27f;
                table.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                myDocument.Open();
                var context = new ElectronicStoresSystemDbContext();
                string specifier = "0,0.00";

                decimal totalSum = 0;
                myDocument.Open();
                var headerFont = FontFactory.GetFont("Arial", 14, Font.BOLD);
                PdfPCell header = new PdfPCell(new Phrase("Aggregate Sales Report", headerFont));
                header.Colspan = 5;
                header.FixedHeight = 27f;
                header.HorizontalAlignment = Element.ALIGN_CENTER;
                header.VerticalAlignment = Element.ALIGN_MIDDLE;
                table.AddCell(header);

                decimal currentSum = 0;
                var font = FontFactory.GetFont("Arial", 14, Font.BOLD);
                //PdfPCell date = new PdfPCell(new Phrase("D", font));
                //date.Colspan = 5;
                //date.FixedHeight = 27f;
                //date.BackgroundColor = new BaseColor(210, 210, 210);
                //date.HorizontalAlignment = Element.ALIGN_LEFT;
                //date.VerticalAlignment = Element.ALIGN_MIDDLE;
                //table.AddCell(date);
                PdfPCell product = new PdfPCell(new Phrase("Product", font));
                product.FixedHeight = 27f;
                product.BackgroundColor = new BaseColor(210, 210, 210);
                product.HorizontalAlignment = Element.ALIGN_LEFT;
                product.VerticalAlignment = Element.ALIGN_MIDDLE;
                table.AddCell(product);
                PdfPCell quantity = new PdfPCell(new Phrase("Quantity", font));
                quantity.FixedHeight = 27f;
                quantity.BackgroundColor = new BaseColor(210, 210, 210);
                quantity.HorizontalAlignment = Element.ALIGN_LEFT;
                quantity.VerticalAlignment = Element.ALIGN_MIDDLE;
                table.AddCell(quantity);
                PdfPCell price = new PdfPCell(new Phrase("Price", font));
                price.FixedHeight = 27f;
                price.BackgroundColor = new BaseColor(210, 210, 210);
                price.HorizontalAlignment = Element.ALIGN_LEFT;
                price.VerticalAlignment = Element.ALIGN_MIDDLE;
                table.AddCell(price);
                PdfPCell location = new PdfPCell(new Phrase("Store", font));
                location.FixedHeight = 27f;
                location.BackgroundColor = new BaseColor(210, 210, 210);
                location.HorizontalAlignment = Element.ALIGN_LEFT;
                location.VerticalAlignment = Element.ALIGN_MIDDLE;
                table.AddCell(location);
                PdfPCell sumCol = new PdfPCell(new Phrase("Sum", font));
                sumCol.FixedHeight = 27f;
                sumCol.BackgroundColor = new BaseColor(210, 210, 210);
                sumCol.HorizontalAlignment = Element.ALIGN_LEFT;
                sumCol.VerticalAlignment = Element.ALIGN_MIDDLE;
                table.AddCell(sumCol);
                foreach (var report in reports)
                {
                    table.AddCell((report.ProductName).ToString());
                    table.AddCell((report.Quantity).ToString());
                    table.AddCell(string.Format("{0:C2}", report.Price));
                    table.AddCell(String.Format("Supermarket \"{0}\"", report.StoreName));
                    PdfPCell sum = new PdfPCell(new Phrase(string.Format("{0:C2}", report.Sum)));
                    sum.HorizontalAlignment = Element.ALIGN_RIGHT;
                    table.AddCell(sum);
                    currentSum += report.Sum;
                    totalSum += report.Sum;
                }
                PdfPCell current = new PdfPCell(new Phrase(string.Format("{0:C2}", currentSum)));
                current.FixedHeight = 27f;
                current.HorizontalAlignment = Element.ALIGN_RIGHT;
                current.VerticalAlignment = Element.ALIGN_MIDDLE;
                table.AddCell(current);
                
                PdfPCell grandTotal = new PdfPCell(new Phrase("Grand total: "));
                grandTotal.Colspan = 3;
                grandTotal.FixedHeight = 27f;
                grandTotal.BackgroundColor = new BaseColor(210, 210, 210);
                grandTotal.HorizontalAlignment = Element.ALIGN_RIGHT;
                grandTotal.VerticalAlignment = Element.ALIGN_MIDDLE;
                table.AddCell(grandTotal);
                PdfPCell total = new PdfPCell(new Phrase(string.Format("{0:C2}", totalSum)));
                total.BackgroundColor = new BaseColor(210, 210, 210);
                total.HorizontalAlignment = Element.ALIGN_RIGHT;
                total.VerticalAlignment = Element.ALIGN_MIDDLE;
                table.AddCell(total);
                myDocument.Add(table);
            }
            catch (DocumentException de)
            {
                Console.WriteLine(de.Message);
            }
            catch (IOException ioe)
            {
                Console.WriteLine(ioe.Message);
            }

            myDocument.Close();
        }
    }
}
