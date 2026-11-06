using Model;

namespace DataAccessLayer
{
    /// <summary>
    /// Фасад CRUD: передает чтение читающему репозиторию, а запись пишущему.
    /// </summary>
    /// <typeparam name="T">Тип объекта, у которого есть ID</typeparam>
    public class Repository<T> : IRepository<T> where T : class, IDomainObject
    {
        IReadRepository<T> reader;
        IWriteRepository<T> writer;

        /// <summary>
        /// Получает репозитории чтения и записи из контейнера.
        /// </summary>
        public Repository(IReadRepository<T> reader, IWriteRepository<T> writer)
        {
            this.reader = reader;
            this.writer = writer;
        }

        /// <summary>
        /// Возвращает все объекты по возрастанию ID.
        /// </summary>
        public List<T> ReadAll()
        {
            return reader.ReadAll();
        }

        /// <summary>
        /// Возвращает объект с указанным ID или null.
        /// </summary>
        public T ReadById(int id)
        {
            return reader.ReadById(id);
        }

        /// <summary>
        /// Добавляет новый объект.
        /// </summary>
        public bool Add(T r)
        {
            return writer.Add(r);
        }

        /// <summary>
        /// Удаляет объект с указанным ID.
        /// </summary>
        public bool Delete(int id)
        {
            return writer.Delete(id);
        }

        /// <summary>
        /// Сохраняет изменения объекта.
        /// </summary>
        public bool Update(T r)
        {
            return writer.Update(r);
        }
    }
}
