using Model;

namespace DataAccessLayer
{
    /// <summary>
    /// Репозиторий: основные CRUD-операции с объектами в базе.
    /// </summary>
    /// <typeparam name="T">Тип объекта, у которого есть ID</typeparam>
    public interface IRepository<T> where T : class, IDomainObject
    {
        /// <summary>
        /// Добавляет объект в базу. После добавления у объекта заполняется ID.
        /// </summary>
        /// <returns>true, если объект добавлен.</returns>
        bool Add(T r);

        /// <summary>
        /// Удаляет объект с указанным ID.
        /// </summary>
        /// <returns>true, если объект удален; false, если такого нет.</returns>
        bool Delete(int id);

        /// <summary>
        /// Возвращает все объекты из базы.
        /// </summary>
        List<T> ReadAll();

        /// <summary>
        /// Возвращает объект с указанным ID или null, если такого нет.
        /// </summary>
        T ReadById(int id);

        /// <summary>
        /// Сохраняет изменения объекта (ищет его по ID).
        /// </summary>
        /// <returns>true, если объект изменен; false, если такого нет.</returns>
        bool Update(T r);
    }
}
