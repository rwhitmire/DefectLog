using System.Data;
using System.Data.Entity;
using System.Linq;

namespace DefectLog.Domain
{
    public interface IRepository<T> where T : class
    {
        T Get(int id);
        IQueryable<T> GetAll();
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Save();
    }

    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _context;

        public Repository(IContextFactory factory)
        {
            _context = factory.GetContext();
        }

        public T Get(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public void Insert(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}