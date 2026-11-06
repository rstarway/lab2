using Model;

namespace DataAccessLayer
{
    /// <summary>
    /// Чтение объектов через Entity Framework.
    /// </summary>
    /// <typeparam name="T">Тип объекта, у которого есть ID</typeparam>
    public class EntityReadRepository<T> : IReadRepository<T> where T : class, IDomainObject
    {
        /// <summary>
        /// Создает таблицу в базе, если ее еще нет.
        /// </summary>
        public EntityReadRepository()
        {
            DBConnection.CreateTable();
        }

        /// <summary>
        /// Возвращает все объекты по возрастанию ID.
        /// </summary>
        public List<T> ReadAll()
        {
            using (DBContext db = new DBContext())
            {
                List<T> list = new List<T>(db.Set<T>());
                list.Sort((a, b) => a.ID.CompareTo(b.ID));
                return list;
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
    }
}
