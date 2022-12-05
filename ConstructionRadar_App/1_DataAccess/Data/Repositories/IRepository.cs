using ConstructionRadar_App.Entities;

namespace ConstructionRadar_App.Repositories
{
    public interface IRepository<T> : IReadRepository<T>, IWriteRepository<T>
        where T : class, IEntity
    {
        public const string auditFileName = "audit.txt";
        public event EventHandler<T>? ItemAdded;
        public event EventHandler<T>? ItemRemoved;
        public event EventHandler<T>? ItemUpdated;
    }
}
