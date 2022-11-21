using ConstructionRadar_App.Entities;

namespace ConstructionRadar_App.Components.TxtReader
{
    public interface ITxtReader
    {
        List<Employee> ReadEmployeesFromFile(string filePath);
    }
}
