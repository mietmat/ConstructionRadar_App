using ConstructionRadar_App.Entities;

namespace ConstructionRadar_App.Components.TxtReader
{
    public interface ITxtWriter
    {
        List<Employee> WriteEmployeeToFile(string filePath);
    }
}
