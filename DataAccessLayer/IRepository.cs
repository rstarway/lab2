using Model;

namespace DataAccessLayer
{
    /// <summary>
    /// Фасад CRUD: объединяет чтение и запись объектов в базе.
    /// </summary>
    /// <typeparam name="T">Тип объекта, у которого есть ID</typeparam>
    public interface IRepository<T> : IReadRepository<T>, IWriteRepository<T> where T : class, IDomainObject
    {
    }
}
