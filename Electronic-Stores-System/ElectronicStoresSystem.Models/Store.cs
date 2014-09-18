namespace ElectronicStoresSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Store
    {
        private ICollection<Sale> sales;

        public Store()
        {
            this.sales = new HashSet<Sale>();
        }

        [Key]
        public int StoreId { get; set; }

        public string StoreName { get; set; }

        public virtual ICollection<Sale> Sales
        {
            get { return this.sales; }
            set { this.sales = value; }
        } 
    }
}
