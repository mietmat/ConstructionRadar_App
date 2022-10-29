using ConstructionRadar_App.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConstructionRadar_App.Repositories
{
    //public delegate void ItemAdded<in T>(T item);

    public class SqlRepository<T> : IRepository<T>
        where T : class, IEntity, new()
    {
        private readonly DbSet<T> _dbSet;
        private readonly DbContext _dbContext;
        private readonly Action<T>? _itemAddedCallback;

        public SqlRepository(DbContext dbContext, Action<T>? itemAddedCallBack = null)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
            _itemAddedCallback = itemAddedCallBack;
        }

        public event EventHandler<T> ItemAdded;

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
            _itemAddedCallback?.Invoke(item);
            ItemAdded?.Invoke(this, item);
        }

        public void AddWithOldId(T item)
        {
            throw new NotImplementedException();
        }

        public void Remove(T item)
        {
            _dbSet.Remove(item);
        }

        public void RemoveAll()
        {
            _dbSet.ToList().Clear();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void Add(T item, List<Employee> items)
        {
            throw new NotImplementedException();
        }
    }
}
