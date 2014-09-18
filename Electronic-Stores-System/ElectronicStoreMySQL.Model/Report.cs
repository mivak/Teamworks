using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElectronicStoreMySQL.Model
{
    public class Report
    {
        public int ReportId { get; set; }

        public string StoreName { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal Sum { get; set; }
    }
}
