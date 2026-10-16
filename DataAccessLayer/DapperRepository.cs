using Dapper;
using Model;
using System.Data.SQLite;

namespace DataAccessLayer
{
    /// <summary>
    /// Репозиторий записей через Dapper (SQL-запросы к таблице Records).
    /// </summary>
    public class DapperRepository : IRepository<Record>
    {
        /// <summary>
        /// Создает таблицу в базе, если ее еще нет.
        /// </summary>
        public DapperRepository()
        {
            DBConnection.CreateTable();
        }

        /// <summary>
        /// Добавляет запись в базу и записывает ей новый ID.
        /// </summary>
        public bool Add(Record r)
        {
            if (r == null)
                return false;
            using (SQLiteConnection con = new SQLiteConnection(DBConnection.str))
            {
                string sql = "insert into Records (Name, Age, Date, Cause, Cell) values (@Name, @Age, @Date, @Cause, @Cell); select last_insert_rowid();";
                r.ID = con.ExecuteScalar<int>(sql, r);
            }
            return r.ID > 0;
        }

        /// <summary>
        /// Удаляет запись с указанным ID.
        /// </summary>
        public bool Delete(int id)
        {
            using (SQLiteConnection con = new SQLiteConnection(DBConnection.str))
            {
                return con.Execute("delete from Records where ID = @id", new { id = id }) > 0;
            }
        }

        /// <summary>
        /// Возвращает все записи.
        /// </summary>
        public List<Record> ReadAll()
        {
            using (SQLiteConnection con = new SQLiteConnection(DBConnection.str))
            {
                return new List<Record>(con.Query<Record>("select * from Records order by ID"));
            }
        }

        /// <summary>
        /// Возвращает запись с указанным ID или null.
        /// </summary>
        public Record ReadById(int id)
        {
            using (SQLiteConnection con = new SQLiteConnection(DBConnection.str))
            {
                return con.QueryFirstOrDefault<Record>("select * from Records where ID = @id", new { id = id });
            }
        }

        /// <summary>
        /// Сохраняет изменения записи.
        /// </summary>
        public bool Update(Record r)
        {
            if (r == null)
                return false;
            using (SQLiteConnection con = new SQLiteConnection(DBConnection.str))
            {
                string sql = "update Records set Name = @Name, Age = @Age, Date = @Date, Cause = @Cause, Cell = @Cell where ID = @ID";
                return con.Execute(sql, r) > 0;
            }
        }
    }
}
