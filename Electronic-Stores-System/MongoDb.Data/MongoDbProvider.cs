namespace MongoDB.Data
{
    using MongoDB.Bson;
    using MongoDB.Driver;
    using MongoDB.Driver.Builders;
    using MongoDB.Driver.Linq;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MongoDb.Data;

    public static class MongoDbProvider
    {

        public static MongoDatabase db
        {
            get
            {
                var connectionstring = dbSetting.Default.MONGOLAB_URI;
                string dbName = MongoUrl.Create(connectionstring).DatabaseName;
                MongoServer dbServer = MongoServer.Create(connectionstring);
                MongoDatabase dbConnection = dbServer.GetDatabase(dbName, WriteConcern.Acknowledged);
                return dbConnection;
            }
        }

        public static IQueryable<T> LoadData<T>(this MongoDatabase source)
        {
            IQueryable<T> result = null;
            try
            {
                result = db.GetCollection<T>(typeof(T).Name).AsQueryable();
            }
            catch (MongoConnectionException ex)
            {
                throw new MongoCommandException("Cannot access database. Please try again later" + ex.Message);
            }
            return result;
        }


        public static void SaveData<T>(this MongoDatabase source, T value)
        {
            try
            {
                MongoDbProvider.db.GetCollection<T>(typeof(T).Name).Save(value, WriteConcern.Acknowledged);
            }
            catch (MongoConnectionException ex)
            {
                throw new MongoCommandException("Cannot access database. Please try again later");
            }
        }

        public static void DeleteData<T>(this MongoDatabase source, string id)
        {
            try
            {
                var result = db.GetCollection<T>(typeof(T).Name).Remove(Query.EQ("_id", new ObjectId(id)));
            }
            catch (MongoConnectionException ex)
            {
                throw new MongoCommandException("Cannot access database. Please try again later");
            }
        }
    }
}
