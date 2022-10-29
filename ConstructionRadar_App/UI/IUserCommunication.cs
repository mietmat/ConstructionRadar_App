using ConstructionRadar_App.Entities;
using ConstructionRadar_App.Repositories;

namespace ConstructionRadar_App.UI
{
    public interface IUserCommunication
    {
        Employee EnterEmployeeName();
        Employee EnterEmployeeSurname();
        void AddEmployeeToFile(Employee employee);
        void UpdateFile(IRepository<Employee> employee);
        Employee DeleteEmployeeFromFile(List<Employee> employees);

    }
}
