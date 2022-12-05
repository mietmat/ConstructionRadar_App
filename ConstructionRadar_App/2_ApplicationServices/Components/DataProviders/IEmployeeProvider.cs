using ConstructionRadar_App.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionRadar_App._2_ApplicationServices.Components.DataProviders
{
    public interface IEmployeeProvider
    {
        List<string> GetUniqueCompany();
        decimal GetEmployeeSalary();
        List<Function> GetFunctions();
        string GetSumOfSalaryForFunction();
        string GetSumOfSalaryForCompany();

        List<Employee> GetEmployeeBySalary();
        List<Employee> GetEmployeeIfSalaryLessThan(decimal money);

    }
}
