namespace MongoDb.Data.Entities
{
    using MongoDB.Bson;

    public class MongoProduct
    {
        public ObjectId _id { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int ManufacturerId { get; set; }

        public decimal BasePrice { get; set; }

        public int CategoryId { get; set; }
    }
}
