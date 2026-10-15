using Model;

namespace DataAccessLayer
{
    /// <summary>
    /// Репозиторий через Entity Framework.
    /// </summary>
    /// <typeparam name="T">Тип объекта, у которого есть ID</typeparam>
    public class EntityRepository<T> : IRepository<T> where T : class, IDomainObject
    {
        /// <summary>
        /// Создает таблицу в базе, если ее еще нет.
        /// </summary>
        public EntityRepository()
        {
            DBConnection.CreateTable();
        }

        /// <summary>
        /// Добавляет объект в базу.
        /// </summary>
        public bool Add(T r)
        {
            using (DBContext db = new DBContext())
            {
                db.Set<T>().Add(r);
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// Удаляет объект с указанным ID.
        /// </summary>
        public bool Delete(int id)
        {
            using (DBContext db = new DBContext())
            {
                T r = db.Set<T>().Find(id);
                if (r == null)
                    return false;
                db.Set<T>().Remove(r);
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// Возвращает все объекты.
        /// </summary>
        public List<T> ReadAll()
        {
            using (DBContext db = new DBContext())
            {
                return new List<T>(db.Set<T>());
            }
        }

        /// <summary>
        /// Возвращает объект с указанным ID или null.
        /// </summary>
        public T ReadById(int id)
        {
            using (DBContext db = new DBContext())
            {
                return db.Set<T>().Find(id);
            }
        }

        /// <summary>
        /// Сохраняет изменения объекта.
        /// </summary>
        public bool Update(T r)
        {
            using (DBContext db = new DBContext())
            {
                T x = db.Set<T>().Find(r.ID);
                if (x == null)
                    return false;
                db.Entry(x).CurrentValues.SetValues(r);
                db.SaveChanges();
                return true;
            }
        }
    }
}
