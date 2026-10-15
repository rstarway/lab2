using Dapper;
using Model;
using System.Data.SQLite;

namespace DataAccessLayer
{
    /// <summary>
    /// Репозиторий через Dapper (обычные SQL-запросы к таблице Records).
    /// </summary>
    /// <typeparam name="T">Тип объекта, у которого есть ID</typeparam>
    public class DapperRepository<T> : IRepository<T> where T : class, IDomainObject
    {
        /// <summary>
        /// Создает таблицу в базе, если ее еще нет.
        /// </summary>
        public DapperRepository()
        {
            DBConnection.CreateTable();
        }

        /// <summary>
        /// Добавляет объект в базу и записывает ему новый ID.
        /// </summary>
        public bool Add(T r)
        {
            using (SQLiteConnection con = new SQLiteConnection(DBConnection.str))
            {
                string sql = "insert into Records (Name, Age, Date, Cause, Cell) values (@Name, @Age, @Date, @Cause, @Cell); select last_insert_rowid();";
                r.ID = con.ExecuteScalar<int>(sql, r);
            }
            return r.ID > 0;
        }

        /// <summary>
        /// Удаляет объект с указанным ID.
        /// </summary>
        public bool Delete(int id)
        {
            using (SQLiteConnection con = new SQLiteConnection(DBConnection.str))
            {
                return con.Execute("delete from Records where ID = @id", new { id = id }) > 0;
            }
        }

        /// <summary>
        /// Возвращает все объекты.
        /// </summary>
        public List<T> ReadAll()
        {
            using (SQLiteConnection con = new SQLiteConnection(DBConnection.str))
            {
                return new List<T>(con.Query<T>("select * from Records order by ID"));
            }
        }

        /// <summary>
        /// Возвращает объект с указанным ID или null.
        /// </summary>
        public T ReadById(int id)
        {
            using (SQLiteConnection con = new SQLiteConnection(DBConnection.str))
            {
                return con.QueryFirstOrDefault<T>("select * from Records where ID = @id", new { id = id });
            }
        }

        /// <summary>
        /// Сохраняет изменения объекта.
        /// </summary>
        public bool Update(T r)
        {
            using (SQLiteConnection con = new SQLiteConnection(DBConnection.str))
            {
                string sql = "update Records set Name = @Name, Age = @Age, Date = @Date, Cause = @Cause, Cell = @Cell where ID = @ID";
                return con.Execute(sql, r) > 0;
            }
        }
    }
}
