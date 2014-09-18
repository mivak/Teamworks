namespace ElectronicStoresSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string ProductName { get; set; }

        [ForeignKey("Manufacturer")]
        public int ManufacturerId { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }

        public decimal BasePrice { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
