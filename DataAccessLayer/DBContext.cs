using Model;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class DBContext : DbContext
    {
        public DBContext() : base("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=lab1;Integrated Security=True;")
        {
            DbProviderFactories.RegisterFactory("System.Data.SqlClient", SqlClientFactory.Instance);
            Database.SetInitializer<DBContext>(null);

            string str = Environment.GetEnvironmentVariable("LAB1_DATABASE");
            if (string.IsNullOrWhiteSpace(str))
            {
                string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "lab1");
                Directory.CreateDirectory(path);
                SqlConnectionStringBuilder con = new SqlConnectionStringBuilder();
                con.DataSource = "(localdb)\\MSSQLLocalDB";
                con.InitialCatalog = "lab1";
                con.AttachDBFilename = Path.Combine(path, "lab1.mdf");
                con.IntegratedSecurity = true;
                str = con.ConnectionString;
            }
            Database.Connection.ConnectionString = str;
        }

        public DbSet<Record> Records { get; set; }
    }
}
