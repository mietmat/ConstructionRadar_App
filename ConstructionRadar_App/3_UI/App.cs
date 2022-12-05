using ConstructionRadar_App._2_ApplicationServices.Components.Services;
using ConstructionRadar_App.Components.DataProviders.Extensions;
using ConstructionRadar_App.Components.TxtReader;
using ConstructionRadar_App.Data;
using ConstructionRadar_App.Entities;
using ConstructionRadar_App.Repositories;
using ConstructionRadar_App.Services;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MotoApp.Components.CsvReader;
using System.Xml.Linq;

namespace ConstructionRadar_App.UI
{
    public class App : IApp
    {
        private readonly IRepository<Employee> _employeesRepository;
        private readonly IRepository<Contract> _contractRepository;
        private readonly ITxtReader _txtReader;
        private readonly IUserCommunication _userCommunication;
        private readonly IEventHandlerService _eventHandlerService;
        private readonly IShowDataProvider _showDataProvider;
        private readonly ICsvReader _csvReader;
        private readonly ConstructionRadarDbContext _constructionRadarDbContext;

        string filePath = "Employees.txt";
        List<Employee> employees = new();
        List<Contract> contracts = new();

        public App(IRepository<Employee> employeeRepository,
            IRepository<Contract> contractRepository,
            ITxtReader txtReader,
            IUserCommunication userCommunication,
            ConstructionRadarDbContext constructionRadarDbContext,
            IEventHandlerService eventHandlerService,
            IShowDataProvider showDataProvider,
            ICsvReader csvReader)
        {
            _employeesRepository = employeeRepository;
            _contractRepository = contractRepository;
            _txtReader = txtReader;
            _userCommunication = userCommunication;
            _constructionRadarDbContext = constructionRadarDbContext;
            _constructionRadarDbContext.Database.EnsureCreated();//make sure DataBase existing / creating DataBase
            _eventHandlerService = eventHandlerService;
            _showDataProvider = showDataProvider;
            _csvReader = csvReader;
        }
        public void Run()
        {
            _eventHandlerService.SubscribeToEvents();

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.White;
                UI_Welcome.AppDescription();

                Console.Write("Please select one option of the 'Main Menu' by clicking assigned number or Q to close application: ");
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
                    bool valid = true;
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
                                ShowEmployees(_employeesRepository);
                                Console.WriteLine($"\nPress any key to open 'Main Menu'");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            case 2:
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
                                emp = _userCommunication.EnterEmployeeCompanyName(emp);
                                emp = _userCommunication.EnterEmployeeSalary(emp);
                                emp.Function = _userCommunication.ChooseEmployeeFunction();
                                _employeesRepository.Add(emp);
                                _employeesRepository.Save();

                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"We have {_employeesRepository.GetAll().Count()} employees !");
                                Console.ForegroundColor = ConsoleColor.White;

                                Console.WriteLine($"Added new employee: {emp.FirstName} {emp.Surname}. Company: {emp.CompanyName}");
                                Console.WriteLine("Press any key to open 'Main Menu' !");
                                Console.ReadKey();

                                Console.Clear();
                                break;
                            case 3:

                                Console.Clear();
                                if (_employeesRepository.GetAll().Count() > 0)
                                {
                                    ShowEmployees(_employeesRepository);
                                }

                                emp = _userCommunication.GetIdToRemoveEmployee();
                                if (emp.FirstName != null)
                                {
                                    _employeesRepository.Remove(emp);
                                    _employeesRepository.Save();
                                }


                                Console.WriteLine("Press any key to open 'Main Menu' !");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            case 4:
                                Console.Clear();
                                if (_employeesRepository.GetAll().Count() > 0)
                                {
                                    ShowEmployees(_employeesRepository);
                                }

                                emp = _userCommunication.GetIdToEditEmployee();
                                if (emp.Id != 0)
                                {
                                    ShowSingleEmployee(emp);
                                }
                                if (emp.FirstName != null)
                                {
                                    var property = _userCommunication.ChooseEmployeePropertyToEdit();
                                    if (property == "Function")
                                    {
                                        emp.Function = _userCommunication.UpdateFunction();
                                    }
                                    else
                                    {
                                        _userCommunication.UpdatePropertyNameEmployee(property);

                                    }
                                    _employeesRepository.Updated(emp);
                                }
                                Console.WriteLine("Press any key to open 'Main Menu' !");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            case 5:
                                Console.Clear();
                                ShowContracts(_contractRepository);
                                Console.WriteLine($"\nPress any key to open 'Main Menu'");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            case 6:
                                Console.Clear();
                                if (_contractRepository.GetAll().Count() > 0)
                                {
                                    ShowContracts(_contractRepository);
                                }
                                else
                                {
                                    Console.WriteLine("Empty list of contracts. Please add new contract !");
                                }

                                Contract contract = new();
                                contract = _userCommunication.EnterContractName(contract);

                                if (contract == null)
                                {
                                    Console.WriteLine("Press any key to open 'Main Menu' !");
                                    Console.ReadKey();
                                    Console.Clear();
                                    break;
                                }


                                contract = _userCommunication.EnterContractCountry(contract);
                                contract = _userCommunication.EnterContractCity(contract);
                                contract = _userCommunication.EnterContractBudget(contract);
                                do
                                {
                                    contract = _userCommunication.EnterContractDate(contract);
                                    valid = CheckData.DateTimeValidation(contract.StartDate, contract.FinishDate);

                                } while (!valid);

                                _contractRepository.Add(contract);
                                _contractRepository.Save();

                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"We have {_contractRepository.GetAll().Count()} contracts !");
                                Console.ForegroundColor = ConsoleColor.White;

                                Console.WriteLine($"Added new contract {contract.Name} in {contract.Country}. We are going to create future in: {contract.City} !");
                                Console.WriteLine("Press any key to open 'Main Menu' !");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            case 7:

                                Console.Clear();
                                if (_contractRepository.GetAll().Count() > 0)
                                {
                                    ShowContracts(_contractRepository);
                                }

                                contract = _userCommunication.GetIdToRemoveContract();
                                if (contract.Name != null)
                                {
                                    _contractRepository.Remove(contract);
                                    _contractRepository.Save();
                                }


                                Console.WriteLine("Press any key to open 'Main Menu' !");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            case 8:
                                Console.Clear();
                                if (_contractRepository.GetAll().Count() > 0)
                                {
                                    ShowContracts(_contractRepository);
                                }

                                contract = _userCommunication.GetIdToEditContract();
                                if (contract.Id != 0)
                                {
                                    ShowSingleContract(contract);
                                }
                                if (contract.Name != null)
                                {
                                    var property = _userCommunication.ChooseContractPropertyToEdit();
                                    do
                                    {
                                        valid = _userCommunication.UpdatePropertyNameContract(property);

                                    } while (!valid);
                                    _contractRepository.Updated(contract);
                                }
                                Console.WriteLine("Press any key to open 'Main Menu' !");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            case 9:
                                Console.Clear();
                                _showDataProvider.ChooseData();
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
            
            CreateXmlHomework();
        }

        private void ShowEmployees(IRepository<Employee> employeesRepository)
        {
            int i = 1;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Current employee list");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine($"LP\tID\tFIRSTNAME\tLASTNAME\tCOMPANY\t\tSALARY\t\tFUNCTION");
            foreach (var employee in employeesRepository.GetAll())
            {
                Console.WriteLine($"{i}.\t{employee.Id}\t{employee.FirstName}\t\t{employee.Surname}\t\t{employee.CompanyName}\t\t{employee.Salary}\t\t{employee.Function}");
                i++;
            }
            Console.WriteLine();
        }

        private void ShowSingleEmployee(Employee employee)
        {
            int i = 1;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Edited employee:");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(employee.ToString());
        }

        private void ShowSingleContract(Contract contract)
        {
            int i = 1;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Edited contract:");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(contract.ToString());
        }

        private void ShowContracts(IRepository<Contract> contractRepository)
        {
            int i = 1;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Current contracts list");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine($"Lp\tId\tName\tCountry\t\tCity\tBudget\t\tStart date\t\tFinish date");
            foreach (var contract in contractRepository.GetAll())
            {
                Console.WriteLine($"{i}.\t{contract.Id}\t{contract.Name}\t{contract.Country}\t\t{contract.City}\t{contract.Budget}\t{contract.StartDate}\t{contract.FinishDate}");
                i++;
            }
            Console.WriteLine();
        }

        public void Close()
        {
            Console.WriteLine("See you again !");
        }

        private void CreateXmlHomework()
        {
            var cars = _csvReader.ProcessCars(@"fuel.csv");
            var manufacturers = _csvReader.ProcessManufacturers(@"manufacturers.csv");
       
            var groups = manufacturers.GroupJoin(
                cars,
                manufacturer => manufacturer.Name,
                car => car.Manufacturer,
                (m, g) =>
                new
                {
                    Manufacturer = m,
                    Cars = g
                })
                .OrderBy(x => x.Manufacturer.Name);

            var document = new XDocument();
            var newXml = new XElement("Manufacturers", groups
                .Select(x =>
                new XElement("Manufacturer",
                new XAttribute("Name", x.Manufacturer.Name),
                new XAttribute("Country", x.Manufacturer.Country),
                new XElement("Cars",
                new XAttribute("country", x.Manufacturer.Country),
                new XAttribute("CombinedSum", x.Cars.Select(x => x.Combined).Sum()),
                x.Cars.Select(s =>
                new XElement("Car",
                new XAttribute("Model", s.Name),
                new XAttribute("Combined", s.Combined)
                    ))))));
            document.Add(newXml);
            document.Save("manufacturer.xml");

        }
    }
}
