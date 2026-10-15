using Model;

namespace DataAccessLayer
{
    public class EntityRepository<T> : IRepository<T> where T : class, IDomainObject
    {
        public bool Add(T r)
        {
            if (r == null)
                return false;
            using (DBContext db = new DBContext())
            {
                db.Set<T>().Add(r);
                return db.SaveChanges() > 0;
            }
        }

        public bool Delete(int id)
        {
            using (DBContext db = new DBContext())
            {
                List<T> list = new List<T>(db.Set<T>());
                foreach (T r in list)
                {
                    if (r.ID == id)
                    {
                        db.Set<T>().Remove(r);
                        return db.SaveChanges() > 0;
                    }
                }
            }
            return false;
        }

        public List<T> ReadAll()
        {
            List<T> res = new List<T>();
            using (DBContext db = new DBContext())
            {
                foreach (T r in db.Set<T>())
                    res.Add(r);
            }
            return res;
        }

        public T ReadById(int id)
        {
            foreach (T r in ReadAll())
            {
                if (r.ID == id)
                    return r;
            }
            return null;
        }

        public bool Update(T r)
        {
            if (r == null)
                return false;
            using (DBContext db = new DBContext())
            {
                List<T> list = new List<T>(db.Set<T>());
                foreach (T x in list)
                {
                    if (x.ID == r.ID)
                    {
                        db.Entry(x).CurrentValues.SetValues(r);
                        db.SaveChanges();
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
