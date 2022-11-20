using ConstructionRadar_App.Data;
using ConstructionRadar_App.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConstructionRadar_App.Repositories
{
    //public delegate void ItemAdded<in T>(T item);

    public class SqlRepository<T> : IRepository<T>
        where T : class, IEntity, new()
    {
        private readonly ConstructionRadarDbContext _constructionRadarDbContext;
        private readonly DbSet<T> _dbSet;
        private readonly Action<T>? _ActionCallback;


        public event EventHandler<T> ItemAdded;
        public event EventHandler<T> ItemRemoved;

        public SqlRepository(ConstructionRadarDbContext constructionRadarDbContext, Action<T>? ActionCallback = null)
        {
            _constructionRadarDbContext = constructionRadarDbContext;
            _dbSet = _constructionRadarDbContext.Set<T>();
            _ActionCallback = ActionCallback;
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T? GetById(int id)
        {

            return _dbSet.Find(id);
        }

        public void Add(T item)
        {
            _dbSet.Add(item);
            _ActionCallback?.Invoke(item);
            ItemAdded?.Invoke(this, item);

        }

        public void AddWithOldId(T item)
        {
            _dbSet.Add(item);
            _ActionCallback?.Invoke(item);
            ItemAdded?.Invoke(this, item);
        }

        public void Remove(T item)
        {
            _dbSet.Remove(item);
            //ItemRemoved?.Invoke(this, item);
        }

        public void RemoveAll()
        {
            _dbSet.ToList().Clear();
        }

        public void Save()
        {
            _constructionRadarDbContext.SaveChanges();
        }

        public void Add(T item, List<Employee> items)
        {
            throw new NotImplementedException();
        }
    }
}
