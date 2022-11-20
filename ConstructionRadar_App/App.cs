using ConstructionRadar_App.Components.TxtReader;
using ConstructionRadar_App.Data;
using ConstructionRadar_App.Entities;
using ConstructionRadar_App.Repositories;
using ConstructionRadar_App.UI;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

namespace ConstructionRadar_App
{
    public class App : IApp
    {
        private readonly IRepository<Employee> _employeesRepository;
        private readonly ITxtReader _txtReader;
        private readonly IUserCommunication _userCommunication;
        private readonly ConstructionRadarDbContext _constructionRadarDbContext;

        string filePath = "Employees.txt";
        List<Employee> employees = new();

        public App(IRepository<Employee> employeeRepository, ITxtReader txtReader, IUserCommunication userCommunication, ConstructionRadarDbContext constructionRadarDbContext)
        {
            _employeesRepository = employeeRepository;
            _txtReader = txtReader;
            _userCommunication = userCommunication;
            _constructionRadarDbContext = constructionRadarDbContext;
            _constructionRadarDbContext.Database.EnsureCreated();//make sure DataBase existing / creating DataBase
        }
        public void Run()
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.White;

                UI_Welcome.AppDescription();
                Console.Write("Please select one option of the 'Main Menu' by clicking assigned number or Q to Quit: ");
                var input = Console.ReadLine();

                if (input.ToString() == "q")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nThanks for using Construction Radar ! See you soon !\nPress any key to close the console view !");
                    Console.ForegroundColor = ConsoleColor.White;

                    break;
                }

                try
                {
                    int selectedOption;
                    if (int.TryParse(input.ToString(), out selectedOption))
                    {
                        UI_Welcome.MainMenuNumber = selectedOption;
                        switch (selectedOption)
                        {
                            case 0:
                                Console.Clear();
                                Console.WriteLine($"Please select number 1-14\nPress any key to open 'Main Menu'");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            case 1:
                                Console.Clear();
                                if (_employeesRepository.GetAll().Count() > 0)
                                {
                                    ShowEmployees(_employeesRepository);
                                }
                                else
                                {
                                    Console.WriteLine("Empty list of employee. Please add new employ !");
                                }

                                Employee emp = new();
                                emp = _userCommunication.EnterEmployeeName(emp);
                                emp = _userCommunication.EnterEmployeeSurname(emp);
                                _employeesRepository.Add(emp);
                                _employeesRepository.Save();

                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"We have {_employeesRepository.GetAll().Count()} employees !");
                                Console.ForegroundColor = ConsoleColor.White;

                                Console.WriteLine($"Added new employee: {emp.FirstName}");
                                Console.WriteLine("Press any key to open 'Main Menu' !");
                                Console.ReadKey();

                                Console.Clear();
                                break;
                            case 2:
                                employees = _employeesRepository.GetAll().ToList();

                                Console.Clear();
                                if (_employeesRepository.GetAll().Count() > 0)
                                {
                                    ShowEmployees(_employeesRepository);
                                }

                                emp = _userCommunication.GetIdToRemoveEmployee();
                                if (emp.FirstName!=null)
                                {
                                    _employeesRepository.Remove(emp);
                                    _employeesRepository.Save();
                                }                               


                                Console.WriteLine("Press any key to open 'Main Menu' !");
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

                    }
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

        }

        private void ShowEmployees(IRepository<Employee> employeesRepository)
        {
            int i = 1;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Current employee list");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine($"Lp    Id   FirstName   LastName");
            foreach (var employee in employeesRepository.GetAll())
            {
                Console.WriteLine($"{i}.    {employee.Id}    {employee.FirstName}    {employee.Surname}");
                i++;
            }
        }

        public void Close()
        {
            Console.WriteLine("See you again !");

        }
    }
}
