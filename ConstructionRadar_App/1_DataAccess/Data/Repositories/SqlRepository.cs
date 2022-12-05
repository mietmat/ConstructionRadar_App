using ConstructionRadar_App.Data;
using ConstructionRadar_App.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ConstructionRadar_App.Repositories
{
    //public delegate void ItemAdded<in T>(T item);

    public class SqlRepository<T> : IRepository<T>
        where T : class, IEntity, new()
    {
        private readonly ConstructionRadarDbContext _constructionRadarDbContext;
        private readonly DbSet<T> _dbSet;
                

        public SqlRepository(ConstructionRadarDbContext constructionRadarDbContext, Action<T>? ActionCallback = null)
        {
            _constructionRadarDbContext = constructionRadarDbContext;
            _dbSet = _constructionRadarDbContext.Set<T>();
        }

        public event EventHandler<T>? ItemAdded;
        public event EventHandler<T>? ItemRemoved;
        public event EventHandler<T>? ItemUpdated;

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
            ItemAdded?.Invoke(this, item);

        }

        public void AddWithOldId(T item)
        {
            _dbSet.Add(item);
            ItemAdded?.Invoke(this, item);
        }

        public void Remove(T item)
        {
            _dbSet.Remove(item);
            ItemRemoved?.Invoke(this, item);
        }

        public void RemoveAll()
        {
            _dbSet.ToList().Clear();
        }

        public void Updated(T item)
        {
            _constructionRadarDbContext.SaveChanges();
            ItemUpdated?.Invoke(this,item);

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
