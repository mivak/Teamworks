namespace ElectronicStoresSystem.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string CategoryName { get; set; }
    }
}
