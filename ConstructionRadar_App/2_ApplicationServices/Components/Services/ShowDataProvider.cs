using ConstructionRadar_App._2_ApplicationServices.Components.DataProviders;
using ConstructionRadar_App.Components.DataProviders.Extensions;
using ConstructionRadar_App.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionRadar_App._2_ApplicationServices.Components.Services
{

    public class ShowDataProvider : IShowDataProvider
    {
        private readonly IEmployeeProvider _employeeProvider;
        public ShowDataProvider(IEmployeeProvider employeeProvider)
        {
            _employeeProvider = employeeProvider;
        }

        string input = "";

        public void ShowMenu()
        {
            Console.WriteLine("Please choose one option (number) with information which do you want to get or Q to open 'Main Menu'\n");
            Console.WriteLine(
                "1.  Show unique company\n" +
                "2.  Show sum of employee salary\n" +
                "3.  Show unique function\n" +
                "4.  Show sum of salary for function\n" +
                "5.  Show sum of salary for company\n" +
                "6.  Order employee by salary\n" +
                "7.  Show employee where salary is less than (your amount of money)\n");

            Console.Write("Your choice: ");

        }

        public void ChooseData()
        {
            bool valid = true;
            while (true)
            {
                ShowMenu();
                input = Console.ReadLine();
                if (input.ToString() == "q")
                {
                    Console.WriteLine("\nThanks for using Construction Radar ! See you soon !\nPress any key to close the console view !");
                    break;
                }

                try
                {
                    int selectedOption;
                    if (int.TryParse(input.ToString(), out selectedOption))
                    {
                        switch (selectedOption)
                        {
                            case 1:
                                ShowUniqueCompany();
                                Console.WriteLine("");
                                Console.WriteLine("Press any key to clear and choose new data !");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            case 2:
                                ShowSumOfSalary();
                                Console.WriteLine("");
                                Console.WriteLine("Press any key to clear and choose new data !");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            case 3:
                                ShowUniqueFunction();
                                Console.WriteLine("");
                                Console.WriteLine("Press any key to clear and choose new data !");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            case 4:
                                ShowSumOfSalaryForFunction();
                                Console.WriteLine("");
                                Console.WriteLine("Press any key to clear and choose new data !");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            case 5:
                                ShowSumOfSalaryForCompany();
                                Console.WriteLine("");
                                Console.WriteLine("Press any key to clear and choose new data !");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            case 6:
                                ShowEmployeeListBySalary();
                                Console.WriteLine("");
                                Console.WriteLine("Press any key to clear and choose new data !");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            case 7:
                                decimal money = GetMoney();
                                ShowEmployeeWhereSalaryLessThan(money);
                                Console.WriteLine("");
                                Console.WriteLine("Press any key to clear and choose new data !");
                                Console.ReadKey();
                                Console.Clear();
                                break;

                            default:
                                Console.Clear();
                                Console.WriteLine($"This option not implemented yet ! Will be soon !\n\n" +
                                    $"Press any key to open 'Main Menu'");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        throw new ArgumentException($"Invalid argument: {nameof(input)}. Please use only numbers!");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void ShowUniqueCompany()
        {
            var uniqueCompany = _employeeProvider.GetUniqueCompany();
            Console.WriteLine();
            foreach (var company in uniqueCompany)
            {
                Console.WriteLine(company.ToString());
            }
            Console.WriteLine();
        }

        public void ShowSumOfSalary()
        {
            var salaryForAllEmployee = _employeeProvider.GetEmployeeSalary();
            Console.WriteLine($"\nSalary for all employee is equal {salaryForAllEmployee}$");

        }

        public void ShowUniqueFunction()
        {
            var functions = _employeeProvider.GetFunctions();
            Console.WriteLine("All functions:\n");

            foreach (var function in functions)
            {
                Console.WriteLine(function);
            }
        }

        public void ShowSumOfSalaryForFunction()
        {
            var sumOfSalaryForFunction = _employeeProvider.GetSumOfSalaryForFunction();
            Console.WriteLine(sumOfSalaryForFunction);
        }
        public void ShowSumOfSalaryForCompany()
        {
            var sumOfSalaryForCompany = _employeeProvider.GetSumOfSalaryForCompany();
            Console.WriteLine(sumOfSalaryForCompany);
        }

        public void ShowEmployeeListBySalary()
        {
            List<Employee> employeeOrderedBySalary = _employeeProvider.GetEmployeeBySalary();
            Console.WriteLine();
            foreach (var employee in employeeOrderedBySalary)
            {
                Console.WriteLine($"Employee {employee.FirstName} {employee.Surname} ({employee.Function}) earns {employee.Salary}$ per month");
            }
        }

        public decimal GetMoney()
        {
            Console.Write("Please write amount of money to get employees list: ");
            input = Console.ReadLine();
            bool valid = CheckData.CheckingDecimalData(input);
            decimal amount = 0;
            if (valid)
            {
                amount = decimal.Parse(input);
                return amount;
            }
            else
            {
                Console.WriteLine("Wrong Data ! Please write only numbers !");
                GetMoney();
            }
            return amount;
        }

        public void ShowEmployeeWhereSalaryLessThan(decimal money)
        {
            List<Employee> employees = _employeeProvider.GetEmployeeIfSalaryLessThan(money);
            foreach (var employee in employees)
            {
                Console.WriteLine(employee.ToString());
            }
            if (employees.Count==0)
            {
                Console.WriteLine($"Nobody earns less than {money}$");
            }
        }
    }
}
