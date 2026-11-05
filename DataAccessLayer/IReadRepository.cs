using Model;

namespace DataAccessLayer
{
    /// <summary>
    /// Чтение объектов из базы.
    /// </summary>
    /// <typeparam name="T">Тип объекта, у которого есть ID</typeparam>
    public interface IReadRepository<T> where T : class, IDomainObject
    {
        /// <summary>
        /// Возвращает все объекты из базы.
        /// </summary>
        List<T> ReadAll();

        /// <summary>
        /// Возвращает объект с указанным ID или null, если такого нет.
        /// </summary>
        T ReadById(int id);
    }
}
