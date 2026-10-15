using Model;
using System.Data.Entity;
using System.Data.SQLite;

namespace DataAccessLayer
{
    [DbConfigurationType(typeof(DBConfig))]
    public class DBContext : DbContext
    {
        public DBContext() : base(new SQLiteConnection("Data Source=:memory:"), true)
        {
            Database.SetInitializer<DBContext>(null);

            string str = Environment.GetEnvironmentVariable("LAB1_DATABASE");
            if (string.IsNullOrWhiteSpace(str))
            {
                string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "lab1");
                Directory.CreateDirectory(path);
                SQLiteConnectionStringBuilder con = new SQLiteConnectionStringBuilder();
                con.DataSource = Path.Combine(path, "lab1.db");
                str = con.ConnectionString;
            }
            Database.Connection.ConnectionString = str;

            SQLiteConnection connection = (SQLiteConnection)Database.Connection;
            connection.Open();
            using (SQLiteCommand command = new SQLiteCommand("create table if not exists Records (Id integer primary key autoincrement, Name text, Age integer not null, Date datetime not null, Cause text, Cell integer not null)", connection))
            {
                command.ExecuteNonQuery();
            }
            connection.Close();
        }

        public DbSet<Record> Records { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Record>().ToTable("Records", "main");
        }
    }
}
