using ConstructionRadar_App.Components.TxtReader;
using ConstructionRadar_App.Data;
using ConstructionRadar_App.Entities;
using ConstructionRadar_App.Repositories;
using ConstructionRadar_App.UI;

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
        Employee emp = new();

        public App(IRepository<Employee> employeeRepository, ITxtReader txtReader, IUserCommunication userCommunication, ConstructionRadarDbContext constructionRadarDbContext)
        {
            _employeesRepository = employeeRepository;
            _txtReader = txtReader;
            _constructionRadarDbContext = constructionRadarDbContext;
            _userCommunication = userCommunication;
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
                                employees = _txtReader.ReadEmployeesFromFile(filePath);
                                _employeesRepository.RemoveAll();

                                foreach (var employee in employees)
                                {
                                    _employeesRepository.Add(employee);
                                }

                                if (_employeesRepository.GetAll().Count() > 0)
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("Current list with employees: ");
                                    Console.ForegroundColor = ConsoleColor.White;

                                    foreach (var employee in _employeesRepository.GetAll())
                                    {
                                        Console.WriteLine($"{employee.Id}. {employee.FirstName} {employee.Surname}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Empty list of employee. Please add new employ !");
                                }
                                emp = _userCommunication.EnterEmployeeName();
                                emp = _userCommunication.EnterEmployeeSurname();
                                _employeesRepository.Add(emp);

                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"We have {_employeesRepository.GetAll().Count()} employees !");
                                Console.ForegroundColor = ConsoleColor.White;

                                _userCommunication.AddEmployeeToFile(emp);
                                Console.WriteLine($"Added new employee: {emp.FirstName}!");
                                Console.WriteLine("Press any key to open 'Main Menu' !");
                                Console.ReadKey();

                                Console.Clear();
                                break;
                            case 2:
                                employees = _txtReader.ReadEmployeesFromFile(filePath);
                                _employeesRepository.RemoveAll();

                                foreach (var employee in employees)
                                {
                                    _employeesRepository.Add(employee);
                                }

                                emp = _userCommunication.DeleteEmployeeFromFile(employees);
                                _employeesRepository.Remove(emp);

                                _userCommunication.AddEmployeesToFile(_employeesRepository);
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


        public void Close()
        {
            Console.WriteLine("See you again !");

        }
    }
}
