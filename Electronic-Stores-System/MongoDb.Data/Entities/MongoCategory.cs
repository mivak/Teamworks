using MongoDB.Bson;
namespace MongoDb.Data.Entities
{
    public class MongoCategory
    {
        public ObjectId _id { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }
    }
}
