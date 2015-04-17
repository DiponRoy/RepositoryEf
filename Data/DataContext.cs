using System.Data.Entity;

namespace Data
{
    public class DataContext : Db.PmsContext
    {
        public DataContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        static DataContext()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<DataContext>());
        }
    }
}
