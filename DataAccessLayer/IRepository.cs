using Model;

namespace DataAccessLayer
{
    public interface IRepository<T> where T : class, IDomainObject
    {
        bool Add(T r);
        bool Delete(int id);
        List<T> ReadAll();
        T ReadById(int id);
        bool Update(T r);
    }
}
