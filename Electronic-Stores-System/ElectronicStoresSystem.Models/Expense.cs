namespace ElectronicStoresSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Expense
    {
        [Key]
        public int ExpenseId { get; set; }

        [ForeignKey("Manufacturer")]
        public int? ManufacturerId { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }

        public string Month { get; set; }

        public decimal? Value { get; set; }
    }
}
