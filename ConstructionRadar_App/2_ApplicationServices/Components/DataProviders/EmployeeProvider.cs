using ConstructionRadar_App.Entities;
using ConstructionRadar_App.Repositories;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionRadar_App._2_ApplicationServices.Components.DataProviders
{
    public class EmployeeProvider : IEmployeeProvider
    {
        private readonly IRepository<Employee> _employeeRepository;
        public EmployeeProvider(IRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public List<Employee> GetEmployeeBySalary()
        {
            var employees = _employeeRepository.GetAll();

            return employees.OrderByDescending(x => x.Salary).ToList();
        }

        public List<Employee> GetEmployeeIfSalaryLessThan(decimal money)
        {
            var employees = _employeeRepository.GetAll();
            return employees.Where(x=>x.Salary<money).OrderByDescending(x=>x.Salary).ToList();
        }

        public decimal GetEmployeeSalary()
        {
            var employees = _employeeRepository.GetAll();
            return employees.Select(x => x.Salary).Sum();
        }

        public List<Function> GetFunctions()
        {
            var employees = _employeeRepository.GetAll();
            return employees.Select(x => x.Function).Distinct().ToList();

        }

        public string GetSumOfSalaryForCompany()
        {
            var employees = _employeeRepository.GetAll();
            var companies = employees.GroupBy(x => x.CompanyName)
                .Select(x => new
                {
                    CompanyName = x.Key,
                    SalarySum = x.Sum(s => s.Salary)
                }).OrderByDescending(x => x.SalarySum);
            StringBuilder sb = new StringBuilder();
            Console.WriteLine();

            foreach (var company in companies)
            {
                sb.AppendLine($"Salary sum for {company.CompanyName} employees is equal {company.SalarySum}$ per month");
            }

            return sb.ToString();
        }

        public string GetSumOfSalaryForFunction()
        {
            var employees = _employeeRepository.GetAll();
            var functions = employees
                .GroupBy(x => x.Function)
                .Select(g => new
                {
                    Name = g.Key,
                    SalarySum = g.Sum(c => c.Salary)
                }).OrderByDescending(x => x.SalarySum);

            StringBuilder sb = new StringBuilder();
            Console.WriteLine();
            foreach (var function in functions)
            {
                sb.AppendLine($"Salary sum for {function.Name}s is equal {function.SalarySum}$ per month");
            }

            return sb.ToString();
        }

        public List<string> GetUniqueCompany()
        {
            var employees = _employeeRepository.GetAll();
            return employees.Select(x => x.CompanyName).Distinct().ToList();
        }
    }
}
