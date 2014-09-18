namespace ElecrtronicStoreSQLiteDB.Model
{
    using System.ComponentModel.DataAnnotations;

    public class AdditionalData
    {
        [Key]
        public int InfoId { get; set; }

        public string InfoDescription { get; set; }

        public decimal Mark { get; set; }
    }
}
