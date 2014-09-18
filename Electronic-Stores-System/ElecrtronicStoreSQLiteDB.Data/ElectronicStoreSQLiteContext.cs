namespace ElecrtronicStoreSQLiteDB.Data
{
    using System.Data.Entity;

    using ElecrtronicStoreSQLiteDB.Data.Migrations;
    using ElecrtronicStoreSQLiteDB.Model;

    public class ElectronicStoreSQLiteContext : DbContext
    {
        public ElectronicStoreSQLiteContext()
            : base("SQLITE_URI")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ElectronicStoreSQLiteContext, Configuration>());
        }

        public IDbSet<AdditionalData> AdditionalDatas { get; set; }
    }
}
