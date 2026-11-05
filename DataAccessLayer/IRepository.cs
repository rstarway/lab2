using Model;

namespace DataAccessLayer
{
    /// <summary>
    /// Фасад CRUD: объединяет чтение и запись объектов в базе.
    /// </summary>
    /// <remarks>
    /// Чтение и запись описаны в IReadRepository и IWriteRepository.
    /// Реализация должна поддерживать хранение типа T. Ошибки доступа к базе передаются вызывающему коду.
    /// </remarks>
    /// <typeparam name="T">Тип объекта, у которого есть ID</typeparam>
    public interface IRepository<T> : IReadRepository<T>, IWriteRepository<T> where T : class, IDomainObject
    {
    }
}
