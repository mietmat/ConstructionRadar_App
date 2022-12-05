using ConstructionRadar_App.Entities;
using ConstructionRadar_App.Repositories;

namespace ConstructionRadar_App.UI
{
    public interface IUserCommunication
    {
        //EMPLOYEE
        Employee EnterEmployeeName(Employee employee);
        Employee EnterEmployeeSurname(Employee employee);
        Employee EnterEmployeeCompanyName(Employee employee);
        Employee EnterEmployeeSalary(Employee employee);
        void AddEmployeeToFile(Employee employee);
        void UpdateFile(IRepository<Employee> employee);
        Employee DeleteEmployeeFromFile(List<Employee> employees);
        Employee GetIdToRemoveEmployee();
        Employee GetIdToEditEmployee();
        string ChooseEmployeePropertyToEdit();
        void UpdatePropertyNameEmployee(string propertyName);
        Function UpdateFunction();
        Function ChooseEmployeeFunction();

        //CONTRACT
        Contract EnterContractName(Contract contract);
        Contract EnterContractCountry(Contract contract);
        Contract EnterContractCity(Contract contract);
        Contract EnterContractBudget(Contract contract);
        Contract EnterContractDate(Contract contract);
        Contract GetIdToRemoveContract();
        Contract GetIdToEditContract();
        string ChooseContractPropertyToEdit();
        bool UpdatePropertyNameContract(string propertyName);



    }
}
