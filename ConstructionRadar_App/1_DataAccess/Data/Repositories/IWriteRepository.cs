using ConstructionRadar_App.Entities;

namespace ConstructionRadar_App.Repositories
{
    public interface IWriteRepository<in T> where T : class, IEntity
    {
        void Add(T item);
        void Add(T item,List<Employee> items);
        void AddWithOldId(T item);
        void Remove(T item);
        void Save();
        void RemoveAll();
    }
}
