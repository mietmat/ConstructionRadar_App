using ConstructionRadar_App.Components.DataProviders.Extensions;
using ConstructionRadar_App.Components.TxtReader;
using ConstructionRadar_App.Repositories;
using Employee = ConstructionRadar_App.Entities.Employee;

namespace ConstructionRadar_App.UI
{
    public class UserCommunication : IUserCommunication
    {
        public ITxtReader _txtReader { get; set; }
        Employee employee = new();

        string filePath = "Employees.txt";
        string actionsFile = $"AllAction.txt";

        public UserCommunication(ITxtReader txtReader)
        {
            _txtReader = txtReader;
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
                if (File.Exists(filePath))
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

        public Employee EnterEmployeeName()
        {
            Console.Write("Please enter name of the employee: ");

            employee.FirstName = Console.ReadLine().ToUpper();
           
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

        public Employee EnterEmployeeSurname()
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

    }
}
