using Dapper;
using Model;
using System.Data.SqlClient;
using System.Reflection;

namespace DataAccessLayer
{
    public class DapperRepository<T> : IRepository<T> where T : class, IDomainObject
    {
        string str;
        string table;
        string key = "ID";

        public DapperRepository()
        {
            table = typeof(T).Name + "s";
            if (typeof(T).GetProperty("Id") != null)
                key = "Id";
            using (DBContext db = new DBContext())
            {
                db.Database.CreateIfNotExists();
                str = db.Database.Connection.ConnectionString;
            }
        }

        public bool Add(T r)
        {
            if (r == null)
                return false;
            string columns = "";
            string values = "";
            foreach (PropertyInfo x in typeof(T).GetProperties())
            {
                if (x.Name != key)
                {
                    if (columns != "")
                    {
                        columns += ", ";
                        values += ", ";
                    }
                    columns += "[" + x.Name + "]";
                    values += "@" + x.Name;
                }
            }
            using (SqlConnection con = new SqlConnection(str))
            {
                r.ID = con.ExecuteScalar<int>("insert into [" + table + "] (" + columns + ") output inserted.[" + key + "] values (" + values + ")", r);
                return r.ID > 0;
            }
        }

        public bool Delete(int id)
        {
            using (SqlConnection con = new SqlConnection(str))
            {
                return con.Execute("delete from [" + table + "] where [" + key + "] = @id", new { id }) > 0;
            }
        }

        public List<T> ReadAll()
        {
            using (SqlConnection con = new SqlConnection(str))
            {
                return new List<T>(con.Query<T>("select * from [" + table + "] order by [" + key + "]"));
            }
        }

        public T ReadById(int id)
        {
            using (SqlConnection con = new SqlConnection(str))
            {
                foreach (T r in con.Query<T>("select * from [" + table + "] where [" + key + "] = @id", new { id }))
                    return r;
            }
            return null;
        }

        public bool Update(T r)
        {
            if (r == null)
                return false;
            string values = "";
            foreach (PropertyInfo x in typeof(T).GetProperties())
            {
                if (x.Name != key)
                {
                    if (values != "")
                        values += ", ";
                    values += "[" + x.Name + "] = @" + x.Name;
                }
            }
            DynamicParameters p = new DynamicParameters(r);
            p.Add("key", r.ID);
            using (SqlConnection con = new SqlConnection(str))
            {
                return con.Execute("update [" + table + "] set " + values + " where [" + key + "] = @key", p) > 0;
            }
        }
    }
}
