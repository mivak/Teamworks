namespace MongoDb.Data.Entities
{
    using MongoDB.Bson;

    public class MongoManufacturer
    {
        public ObjectId _id { get; set; }

        public int ManufacturerId { get; set; }

        public string ManufacturerName { get; set; }
    }
}
