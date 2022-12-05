using ConstructionRadar_App.Components.DataProviders.Extensions;
using ConstructionRadar_App.Components.TxtReader;
using ConstructionRadar_App.Data;
using ConstructionRadar_App.Entities;
using ConstructionRadar_App.Repositories;
using System;
using Employee = ConstructionRadar_App.Entities.Employee;

namespace ConstructionRadar_App.UI
{
    public class UserCommunication : IUserCommunication
    {
        public ITxtReader _txtReader { get; set; }
        public ConstructionRadarDbContext _constructionRadarDbContext { get; set; }
        Employee employee = new();
        Contract contract = new();

        string filePath = "Employees.txt";
        string actionsFile = $"AllAction.txt";

        public UserCommunication(ITxtReader txtReader, ConstructionRadarDbContext constructionRadarDbContext)
        {
            _txtReader = txtReader;
            _constructionRadarDbContext = constructionRadarDbContext;
        }

        public void AddEmployeeToFile(Employee employee)
        {
            if (!File.Exists(filePath))
                using (var allEmployee = File.Create(filePath))
                {
                }
            Console.Clear();

            using (var allEmployee = File.AppendText(filePath))
            {
                allEmployee.WriteLine($"{employee.Id} {employee.FirstName} {employee.Surname}");
            }

            using (var allActions = File.AppendText(actionsFile))
            {
                allActions.WriteLine($"{DateTime.Now}-EmployeeAdded- Id:{employee.Id}. {employee.FirstName} {employee.Surname}");
            }

        }

        public void UpdateFile(IRepository<Employee> employees)
        {
            List<Employee> employeeList = employees.GetAll().ToList();


            if (!File.Exists(filePath))
                using (var allEmployee = File.Create(filePath))
                {
                }
            Console.Clear();

            foreach (var employee in employeeList)
            {
                using (var allEmployee = File.AppendText(filePath))
                {
                    allEmployee.WriteLine($"{employee.Id} {employee.FirstName} {employee.Surname}");
                }
            }
        }

        public Employee DeleteEmployeeFromFile(List<Employee> employees)
        {
            bool valid = true;
            int numberToRemove;
            string input;
            do
            {
                if (true)
                {

                    do
                    {
                        Console.Write($"If you want to remove employee use number min:{employees.Min(x => x.Id)}, max:" +
                                          $" {employees.Max(x => x.Id)}\nPlease write id to delete employee or Q to Quit: ");
                        input = Console.ReadLine();
                        if (input.ToLower() == "q")
                        {
                            break;
                        }
                        valid = CheckData.CheckingIntData(input);
                        if (valid)
                        {
                            numberToRemove = int.Parse(input);
                            try
                            {
                                employees.First(x => x.Id == numberToRemove);

                            }
                            catch (Exception ex)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"ID: {numberToRemove} doesn't exist. Error: {ex.Message}");
                                Console.ForegroundColor = ConsoleColor.White;

                                valid = false;
                            }
                        }

                    } while (!valid);
                    if (input.ToLower() == "q")
                    {
                        employee.Surname = null;
                        employee.FirstName = null;

                        break;
                    }
                    numberToRemove = int.Parse(input);
                    employee = employees.FirstOrDefault(x => x.Id == numberToRemove);

                    File.Delete(filePath);

                    if (numberToRemove <= 0)
                    {
                        Console.WriteLine($"Please use number > 0");
                    }

                    Console.Clear();

                    using (var allActions = File.AppendText(actionsFile))
                    {
                        allActions.WriteLine($"{DateTime.Now}-EmployeeDeleted- Id:{employee.Id}, FirstName: {employee.FirstName},LastName: {employee.Surname}");
                    }


                    return (Employee)employee;
                }
                else
                {
                    Console.WriteLine("You can't delete the employee - file doesn't exist ! Please choose another option from 'Main Menu' !\n\nPress any key to open 'Main Menu' !\n");
                    Console.ReadKey();
                }
            } while (true);

            return employee;

        }

        public Employee EnterEmployeeName(Employee employee)
        {

            Console.Write("Please enter name of the employee: ");
            var input = Console.ReadLine().ToUpper();

            if (input != "Q")
            {
                employee.FirstName = input;

                while (!CheckData.CheckingStringData(employee.FirstName))
                {
                    Console.Write("Employee name should have only a letters ! Please enter name of the employee: ");
                    employee.FirstName = Console.ReadLine().ToUpper();
                }

                if (employee.FirstName.ToLower() == "q")
                {
                    Environment.Exit(0);
                }
                return employee;
            }


            return employee;
        }

        public Employee EnterEmployeeSurname(Employee employee)
        {
            Console.Write("Please enter surname of the employee: ");

            employee.Surname = Console.ReadLine().ToUpper();
            while (!CheckData.CheckingStringData(employee.Surname))
            {
                Console.Write("Employee surname should have only a letters ! Please enter surname of the employee: ");
                employee.Surname = Console.ReadLine().ToUpper();
            }

            if (employee.Surname.ToLower() == "q")
            {
                Environment.Exit(0);
            }

            return employee;
        }

        public Employee EnterEmployeeCompanyName(Employee employee)
        {
            Console.Write("Please enter company name: ");

            employee.CompanyName = Console.ReadLine().ToUpper();

            if (employee.CompanyName.ToLower() == "q")
            {
                Environment.Exit(0);
            }
            return employee;
        }

        public Employee EnterEmployeeSalary(Employee employee)
        {
            Console.Write("Please enter salary: ");

            var salary = Console.ReadLine().ToUpper();

            while (!CheckData.CheckingIntData(salary))
            {
                Console.Write("Salary should have only a numbers ! Please enter salary: ");
                salary = Console.ReadLine().ToUpper();
            }
            if (salary.ToLower() == "q")
            {
                Environment.Exit(0);
            }
            employee.Salary = decimal.Parse(salary);

            return employee;
        }

        public Employee GetIdToRemoveEmployee()
        {
            bool valid = true;
            int numberToRemove;
            string input;
            do
            {
                if (_constructionRadarDbContext.Employees != null)
                {
                    do
                    {
                        Console.Write($"If you want to remove employee use number min:{_constructionRadarDbContext.Employees.First().Id}, max:" +
                                          $" {_constructionRadarDbContext.Employees.OrderBy(x => x.Id).Last().Id}\nPlease write id to delete employee or Q to Quit: ");
                        input = Console.ReadLine();
                        if (input.ToLower() == "q")
                        {
                            break;
                        }
                        valid = CheckData.CheckingIntData(input);
                        if (valid)
                        {
                            numberToRemove = int.Parse(input);
                            try
                            {
                                _constructionRadarDbContext.Employees.First(x => x.Id == numberToRemove);

                            }
                            catch (Exception ex)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"ID: {numberToRemove} doesn't exist. Error: {ex.Message}");
                                Console.ForegroundColor = ConsoleColor.White;

                                valid = false;
                            }
                        }

                    } while (!valid);
                    if (input.ToLower() == "q")
                    {
                        employee.Surname = null;
                        employee.FirstName = null;

                        break;
                    }
                    numberToRemove = int.Parse(input);

                    if (numberToRemove <= 0)
                    {
                        Console.WriteLine($"Please use number > 0");
                    }

                    Console.Clear();

                    employee = _constructionRadarDbContext.Employees.FirstOrDefault(x => x.Id == numberToRemove);

                    return employee;
                }
                else
                {
                    Console.WriteLine("You can't delete - employee doesn't exist ! Please choose another option from 'Main Menu' !\n\nPress any key to open 'Main Menu' !\n");
                    Console.ReadKey();
                }
            } while (true);

            return employee;
        }
        public Employee GetIdToEditEmployee()
        {
            bool valid = true;
            int numberToEdit;
            string input;
            do
            {
                if (_constructionRadarDbContext.Employees != null)
                {
                    do
                    {
                        Console.Write($"\nIf you want to edit employee use number min:{_constructionRadarDbContext.Employees.First().Id}, max:" +
                                          $" {_constructionRadarDbContext.Employees.OrderBy(x => x.Id).Last().Id}\nPlease write employee id to edit or Q to Quit: ");
                        input = Console.ReadLine();
                        if (input.ToLower() == "q")
                        {
                            break;
                        }
                        valid = CheckData.CheckingIntData(input);
                        if (valid)
                        {
                            numberToEdit = int.Parse(input);
                            try
                            {
                                _constructionRadarDbContext.Employees.First(x => x.Id == numberToEdit);

                            }
                            catch (Exception ex)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"ID: {numberToEdit} doesn't exist. Error: {ex.Message}");
                                Console.ForegroundColor = ConsoleColor.White;

                                valid = false;
                            }
                        }

                    } while (!valid);
                    if (input.ToLower() == "q")
                    {
                        employee.Surname = null;
                        employee.FirstName = null;

                        break;
                    }
                    numberToEdit = int.Parse(input);

                    if (numberToEdit <= 0)
                    {
                        Console.WriteLine($"Please use number > 0");
                    }

                    Console.Clear();

                    employee = _constructionRadarDbContext.Employees.FirstOrDefault(x => x.Id == numberToEdit);

                    return employee;
                }
                else
                {
                    Console.WriteLine("You can't edit - employee doesn't exist ! Please choose another option from 'Main Menu' !\n\nPress any key to open 'Main Menu' !\n");
                    Console.ReadKey();
                }
            } while (true);

            return employee;
        }

        public string ChooseEmployeePropertyToEdit()
        {
            string tempInput;
            bool valid = true;
            string userProperty = "";
            do
            {
                if (_constructionRadarDbContext.Employees != null)
                {
                    do
                    {
                        Console.Write($"If you want to edit please choose property number, min: {(int)EmployeeProperty.FirstName}, max:" +
                                          $" {(int)EmployeeProperty.Salary}\nPlease write number to edit or Q to Quit: ");
                        tempInput = Console.ReadLine();
                        if (tempInput.ToLower() == "q")
                        {
                            break;
                        }
                        valid = CheckData.CheckingIntData(tempInput);
                        if (valid)
                        {
                            var input = int.Parse(tempInput);
                            try
                            {
                                if (input == 1)
                                {
                                    userProperty = EmployeeProperty.FirstName.ToString();
                                }
                                else if (input == 2)
                                {
                                    userProperty = EmployeeProperty.SurName.ToString();
                                }
                                else if (input == 3)
                                {
                                    userProperty = EmployeeProperty.CompanyName.ToString();
                                }
                                else if (input == 4)
                                {
                                    userProperty = EmployeeProperty.Salary.ToString();
                                }
                                else if (input == 5)
                                {
                                    userProperty = EmployeeProperty.Function.ToString();
                                }
                                else
                                {
                                    throw new Exception();
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"Property: {input} doesn't exist. Error: {ex.Message}");
                                Console.ForegroundColor = ConsoleColor.White;

                                valid = false;
                            }
                        }

                    } while (!valid);
                    if (tempInput.ToLower() == "q")
                    {
                        employee.Surname = null;
                        employee.FirstName = null;

                        break;
                    }
                }
                else
                {
                    Console.WriteLine("You can't delete - employee doesn't exist ! Please choose another option from 'Main Menu' !\n\nPress any key to open 'Main Menu' !\n");
                    Console.ReadKey();
                }
                return userProperty;

            } while (true);
            return userProperty;

        }

        public void UpdatePropertyNameEmployee(string propertyName)
        {
            Console.Write($"Please enter new '{propertyName}': ");
            var newName = Console.ReadLine().ToUpper();

            bool valid = CheckData.CheckingDecimalData(newName);

            if (propertyName == Enum.GetName(EmployeeProperty.FirstName).ToString())
            {
                employee.FirstName = newName;

            }
            else if (propertyName == Enum.GetName(EmployeeProperty.SurName).ToString())
            {
                employee.Surname = newName;
            }
            else if (propertyName == Enum.GetName(EmployeeProperty.CompanyName).ToString())
            {
                employee.CompanyName = newName;
            }
            else if (propertyName == Enum.GetName(EmployeeProperty.Function).ToString())
            {
                if (propertyName == Function.ProjectManager.ToString())
                {
                    employee.Function = Function.ProjectManager;

                }
                else if (propertyName == Function.TeamLeader.ToString())
                {
                    employee.Function = Function.TeamLeader;

                }
                else if (propertyName == Function.Engineer.ToString())
                {
                    employee.Function = Function.Engineer;

                }
                else if (propertyName == Function.Worker.ToString())
                {
                    employee.Function = Function.Worker;

                }
            }
            if (valid)
            {
                employee.Salary = decimal.Parse(newName);
            }
            _constructionRadarDbContext.Employees.Update(employee);

        }

        public Function ChooseEmployeeFunction()
        {
            int inputFunction;
            Function function = Function.Worker;
            Console.WriteLine($"Press number to choose employee function:\n" +
                $"1.{Function.ProjectManager}\n" +
                $"2.{Function.TeamLeader}\n" +
                $"3.{Function.Engineer}\n" +
                $"4.{Function.Worker}");

            var input = Console.ReadLine();

            bool valid = CheckData.CheckingIntData(input);
            if (valid)
            {
                var functionInput = int.Parse(input);
                try
                {
                    if (functionInput == 1)
                    {
                        function = Function.ProjectManager;

                    }
                    else if (functionInput == 2)
                    {
                        function = Function.TeamLeader;
                    }
                    else if (functionInput == 3)
                    {
                        function = Function.Engineer;
                    }
                    else if (functionInput == 4)
                    {
                        function = Function.Worker;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Function: {input} doesn't exist. Error: {ex.Message}");
                    Console.ForegroundColor = ConsoleColor.White;

                    valid = false;
                }

            }

            return function;

        }

        public Function UpdateFunction()
        {
            return ChooseEmployeeFunction();
        }

        public Contract EnterContractName(Contract contract)
        {
            Console.Write("Please enter contract name or Q to exit: ");
            var input = Console.ReadLine().ToUpper();

            if (input.ToLower() == "q")
            {
                return null;
            }

            while (!CheckData.CheckingStringData(input) || String.IsNullOrEmpty(input))
            {
                Console.Write("Contract name should have only a letters ! Please enter contract name: ");
                input = Console.ReadLine().ToUpper();
            }
            contract.Name = input;

            return contract;
        }

        public Contract EnterContractCountry(Contract contract)
        {
            Console.Write("Please enter contract country: ");
            contract.Country = Console.ReadLine().ToUpper();

            while (!CheckData.CheckingStringData(contract.Country) || String.IsNullOrEmpty(contract.Country))
            {
                Console.Write("Contract country should have only a letters ! Please enter contract country: ");
                contract.Country = Console.ReadLine().ToUpper();
            }

            if (contract.Country.ToLower() == "q")
            {
                Environment.Exit(0);
            }
            return contract;
        }


        public Contract EnterContractCity(Contract contract)
        {
            Console.Write("Please enter contract city: ");
            contract.City = Console.ReadLine().ToUpper();

            while (!CheckData.CheckingStringData(contract.City) || String.IsNullOrEmpty(contract.City))
            {
                Console.Write("Contract city should have only a letters ! Please enter contract city: ");
                contract.City = Console.ReadLine().ToUpper();
            }

            if (contract.City.ToLower() == "q")
            {
                Environment.Exit(0);
            }
            return contract;
        }

        public Contract EnterContractBudget(Contract contract)
        {
            Console.Write("Please enter contract budget: ");
            var input = Console.ReadLine().ToUpper();

            if (input.ToLower() == "q")
            {
                Environment.Exit(0);
            }

            while (!CheckData.CheckingDecimalData(input))
            {
                Console.Write("Budget should have only a numbers ! Please enter contract budget: ");
                input = Console.ReadLine().ToUpper();
            }

            contract.Budget = decimal.Parse(input);

            return contract;
        }

        public Contract EnterContractDate(Contract contract)
        {
            Console.Write("Please enter contract start date format(dd/mm/yyyy): ");
            var input = Console.ReadLine();

            while (!CheckData.CheckingDateTimeData(input))
            {
                Console.Write("Wrong format ! Please enter contract start date format(dd/mm/yyyy): ");
                input = Console.ReadLine().ToUpper();
            }
            contract.StartDate = DateTime.Parse(input);

            Console.Write("Please enter contract finish date format(dd/mm/yyyy): ");
            input = Console.ReadLine();

            while (!CheckData.CheckingDateTimeData(input))
            {
                Console.Write("Wrong format ! Please enter contract finish date format(dd/mm/yyyy): ");
                input = Console.ReadLine().ToUpper();
            }

            contract.FinishDate = DateTime.Parse(input);

            var valid = CheckData.DateTimeValidation(contract.StartDate, contract.FinishDate);

            if (!valid)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong date ! Please provide correct date. Start date can't be later than finish date !");
                Console.ForegroundColor = ConsoleColor.White;

            }

            if (contract.StartDate.ToString().ToLower() == "q")
            {
                Environment.Exit(0);
            }      
                        
            return contract;
        }

        public Contract GetIdToRemoveContract()
        {
            bool valid = true;
            int numberToRemove;
            string input;
            do
            {
                if (_constructionRadarDbContext.Contract != null)
                {
                    do
                    {
                        Console.Write($"If you want to remove contract use number min:{_constructionRadarDbContext.Contract.First().Id}, max:" +
                                          $" {_constructionRadarDbContext.Contract.OrderBy(x => x.Id).Last().Id}\nPlease write id to delete contract or Q to Quit: ");
                        input = Console.ReadLine();
                        if (input.ToLower() == "q")
                        {
                            break;
                        }
                        valid = CheckData.CheckingIntData(input);
                        if (valid)
                        {
                            numberToRemove = int.Parse(input);
                            try
                            {
                                _constructionRadarDbContext.Contract.First(x => x.Id == numberToRemove);

                            }
                            catch (Exception ex)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"ID: {numberToRemove} doesn't exist. Error: {ex.Message}");
                                Console.ForegroundColor = ConsoleColor.White;

                                valid = false;
                            }
                        }

                    } while (!valid);
                    if (input.ToLower() == "q")
                    {
                        break;
                    }
                    numberToRemove = int.Parse(input);

                    if (numberToRemove <= 0)
                    {
                        Console.WriteLine($"Please use number > 0");
                    }

                    Console.Clear();

                    contract = _constructionRadarDbContext.Contract.FirstOrDefault(x => x.Id == numberToRemove);

                    return contract;
                }
                else
                {
                    Console.WriteLine("You can't delete - contract doesn't exist ! Please choose another option from 'Main Menu' !\n\nPress any key to open 'Main Menu' !\n");
                    Console.ReadKey();
                }
            } while (true);

            return contract;
        }

        public Contract GetIdToEditContract()
        {
            bool valid = true;
            int numberToEdit;
            string input;
            do
            {
                if (_constructionRadarDbContext.Contract != null)
                {
                    do
                    {
                        Console.Write($"\nIf you want to edit contract use number min:{_constructionRadarDbContext.Contract.First().Id}, max:" +
                                          $" {_constructionRadarDbContext.Contract.OrderBy(x => x.Id).Last().Id}\nPlease write contract id to edit or Q to Quit: ");
                        input = Console.ReadLine();
                        if (input.ToLower() == "q")
                        {
                            break;
                        }
                        valid = CheckData.CheckingIntData(input);
                        if (valid)
                        {
                            numberToEdit = int.Parse(input);
                            try
                            {
                                _constructionRadarDbContext.Contract.First(x => x.Id == numberToEdit);
                            }
                            catch (Exception ex)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"ID: {numberToEdit} doesn't exist. Error: {ex.Message}");
                                Console.ForegroundColor = ConsoleColor.White;
                                valid = false;
                            }
                        }

                    } while (!valid);
                    if (input.ToLower() == "q")
                    {
                        contract.Name = null;
                        break;
                    }
                    numberToEdit = int.Parse(input);

                    if (numberToEdit <= 0)
                    {
                        Console.WriteLine($"Please use number > 0");
                    }

                    Console.Clear();

                    contract = _constructionRadarDbContext.Contract.FirstOrDefault(x => x.Id == numberToEdit);

                    return contract;
                }
                else
                {
                    Console.WriteLine("You can't edit - employee doesn't exist ! Please choose another option from 'Main Menu' !\n\nPress any key to open 'Main Menu' !\n");
                    Console.ReadKey();
                }
            } while (true);

            return contract;
        }

        public string ChooseContractPropertyToEdit()
        {
            string tempInput;
            bool valid = true;
            string userProperty = "";
            do
            {
                if (_constructionRadarDbContext.Contract != null)
                {
                    do
                    {
                        Console.Write($"If you want to edit please choose property number, min: {(int)ContractProperty.Name}, max:" +
                                          $" {(int)ContractProperty.FinishDate}\nPlease write number to edit or Q to Quit: ");
                        tempInput = Console.ReadLine();
                        if (tempInput.ToLower() == "q")
                        {
                            break;
                        }
                        valid = CheckData.CheckingIntData(tempInput);
                        if (valid)
                        {
                            var input = int.Parse(tempInput);
                            try
                            {
                                if (input == 1)
                                {
                                    userProperty = ContractProperty.Name.ToString();
                                }
                                else if (input == 2)
                                {
                                    userProperty = ContractProperty.Country.ToString();
                                }
                                else if (input == 3)
                                {
                                    userProperty = ContractProperty.City.ToString();
                                }
                                else if (input == 4)
                                {
                                    userProperty = ContractProperty.Budget.ToString();
                                }
                                else if (input == 5)
                                {
                                    userProperty = ContractProperty.StartDate.ToString();
                                }
                                else if (input == 6)
                                {
                                    userProperty = ContractProperty.FinishDate.ToString();
                                }
                                else
                                {
                                    throw new Exception();
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"Property: {input} doesn't exist. Error: {ex.Message}");
                                Console.ForegroundColor = ConsoleColor.White;

                                valid = false;
                            }
                        }

                    } while (!valid);
                    if (tempInput.ToLower() == "q")
                    {
                        employee.Surname = null;
                        employee.FirstName = null;

                        break;
                    }
                }
                else
                {
                    Console.WriteLine("You can't delete - employee doesn't exist ! Please choose another option from 'Main Menu' !\n\nPress any key to open 'Main Menu' !\n");
                    Console.ReadKey();
                }
                return userProperty;

            } while (true);
            return userProperty;
        }

         bool IUserCommunication.UpdatePropertyNameContract(string propertyName)
        {
            Console.Write($"Please enter new '{propertyName}': ");
            var newName = Console.ReadLine().ToUpper();

            bool valid = CheckData.CheckingDecimalData(newName);

            if (propertyName == Enum.GetName(ContractProperty.Name).ToString())
            {
                contract.Name = newName;
            }
            else if (propertyName == Enum.GetName(ContractProperty.Country).ToString())
            {
                contract.Country = newName;

            }
            else if (propertyName == Enum.GetName(ContractProperty.City).ToString())
            {
                contract.City = newName;

            }
            else if (propertyName == Enum.GetName(ContractProperty.StartDate).ToString())
            {
                valid = CheckData.CheckingDateTimeData(newName);
                if (valid)
                {
                    contract.StartDate = DateTime.Parse(newName);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Wrong date ! New Date not saved ! Please edit again !");
                    Console.ForegroundColor = ConsoleColor.White;
                    return false;
                }

                if (!CheckData.DateTimeValidation(contract.StartDate, contract.FinishDate))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Wrong date ! New Date not saved ! Start Date can't be later than Finish Date! Please edit again !");
                    Console.ForegroundColor = ConsoleColor.White;
                    return false;
                }


            }
            else if (propertyName == Enum.GetName(ContractProperty.FinishDate).ToString())
            {
                valid = CheckData.CheckingDateTimeData(newName);
                if (valid)
                {
                    contract.FinishDate = DateTime.Parse(newName);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Wrong date ! New Date not saved ! Please edit again !");
                    Console.ForegroundColor = ConsoleColor.White;
                    return false;
                }

                if (!CheckData.DateTimeValidation(contract.StartDate, contract.FinishDate))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Wrong date ! New Date not saved ! Start Date can't be later than Finish Date! Please edit again !");
                    Console.ForegroundColor = ConsoleColor.White;
                    return false;
                }

            }
            else if (valid)
            {
                contract.Budget = decimal.Parse(newName);
            }           

            _constructionRadarDbContext.Contract.Update(contract);
            return true;

        }
    }
}
