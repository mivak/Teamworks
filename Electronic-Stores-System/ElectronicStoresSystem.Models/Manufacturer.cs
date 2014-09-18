namespace ElectronicStoresSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Manufacturer
    {
        private ICollection<Expense> expenses;

        public Manufacturer()
        {
            this.expenses = new HashSet<Expense>();
        }

        public int ManufacturerId { get; set; }

        public string ManufacturerName { get; set; }

        public virtual ICollection<Expense> Expenses
        {
            get { return this.expenses; }
            set { this.expenses = value; }
        }   
    }
}
