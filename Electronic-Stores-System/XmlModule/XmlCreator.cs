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
    using ElectronicStoreMySQL.Model;

    public class XmlCreator
    {
        public static void CreateXml(List<Report> reports)
        {
            Console.WriteLine("Generating XML report....");
            XmlTextWriter writer = new XmlTextWriter(@"..\..\..\reports.xml", Encoding.UTF8);
            writer.WriteStartDocument(true);
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 2;
            writer.WriteStartElement("Reports");
            foreach (var report in reports)
            {
                createNode(report, writer);
            }
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();
            Console.WriteLine("XML File report created ! ");
        }

        private static void createNode(Report report, XmlTextWriter writer)
        {
            writer.WriteStartElement("Product");
            writer.WriteStartElement("Report-id");
            writer.WriteString(report.ReportId.ToString());
            writer.WriteEndElement();
            writer.WriteStartElement("Product-Name");
            writer.WriteString(report.ProductName);
            writer.WriteEndElement();
            writer.WriteStartElement("Store-Name");
            writer.WriteString(report.StoreName);
            writer.WriteEndElement();
            writer.WriteStartElement("Quantity");
            writer.WriteString(report.Quantity.ToString());
            writer.WriteEndElement();
            writer.WriteStartElement("Price");
            writer.WriteString(report.Price.ToString());
            writer.WriteEndElement();
            writer.WriteStartElement("Sum");
            writer.WriteString(report.Sum.ToString());
            writer.WriteEndElement();
            writer.WriteEndElement();
        }
    }
}
