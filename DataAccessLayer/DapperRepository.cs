using Dapper;
using Model;
using System.Data.SQLite;
using System.Reflection;

namespace DataAccessLayer
{
    /// <summary>
    /// Репозиторий через Dapper (обычные SQL-запросы).
    /// Таблица называется как класс с буквой s на конце (Record - Records),
    /// столбцы называются как свойства класса.
    /// </summary>
    /// <typeparam name="T">Тип объекта, у которого есть ID</typeparam>
    public class DapperRepository<T> : IRepository<T> where T : class, IDomainObject
    {
        string table = typeof(T).Name + "s";

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
            if (r == null)
                return false;

            string columns = "";
            string values = "";
            foreach (PropertyInfo p in typeof(T).GetProperties())
            {
                if (p.Name == "ID")
                    continue;
                if (columns != "")
                {
                    columns += ", ";
                    values += ", ";
                }
                columns += p.Name;
                values += "@" + p.Name;
            }

            using (SQLiteConnection con = new SQLiteConnection(DBConnection.str))
            {
                string sql = "insert into " + table + " (" + columns + ") values (" + values + "); select last_insert_rowid();";
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
                return con.Execute("delete from " + table + " where ID = @id", new { id = id }) > 0;
            }
        }

        /// <summary>
        /// Возвращает все объекты.
        /// </summary>
        public List<T> ReadAll()
        {
            using (SQLiteConnection con = new SQLiteConnection(DBConnection.str))
            {
                return new List<T>(con.Query<T>("select * from " + table + " order by ID"));
            }
        }

        /// <summary>
        /// Возвращает объект с указанным ID или null.
        /// </summary>
        public T ReadById(int id)
        {
            using (SQLiteConnection con = new SQLiteConnection(DBConnection.str))
            {
                return con.QueryFirstOrDefault<T>("select * from " + table + " where ID = @id", new { id = id });
            }
        }

        /// <summary>
        /// Сохраняет изменения объекта.
        /// </summary>
        public bool Update(T r)
        {
            if (r == null)
                return false;

            string values = "";
            foreach (PropertyInfo p in typeof(T).GetProperties())
            {
                if (p.Name == "ID")
                    continue;
                if (values != "")
                    values += ", ";
                values += p.Name + " = @" + p.Name;
            }

            using (SQLiteConnection con = new SQLiteConnection(DBConnection.str))
            {
                string sql = "update " + table + " set " + values + " where ID = @ID";
                return con.Execute(sql, r) > 0;
            }
        }
    }
}
