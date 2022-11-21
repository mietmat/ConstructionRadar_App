using ConstructionRadar_App.Entities;

namespace ConstructionRadar_App.Repositories
{
    public interface IRepository<T> : IReadRepository<T>, IWriteRepository<T>
        where T : class, IEntity
    {

    }
}
