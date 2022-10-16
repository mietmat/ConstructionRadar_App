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
                allActions.WriteLine($"{DateTime.UtcNow}-EmployeeAdded- {employee.Id}. {employee.FirstName} {employee.Surname}");
            }

        }

        public void AddEmployeesToFile(IRepository<Employee> employees)
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

                using (var allActions = File.AppendText(actionsFile))
                {
                    allActions.WriteLine($"{DateTime.UtcNow}-EmployeeAdded- Id: {employee.Id}. FirstName: {employee.FirstName}, Surname: {employee.Surname}");
                }
            }



        }

        public Employee DeleteEmployeeFromFile(List<Employee> employees)
        {
            bool valid = true;
            int numberToRemove;
            string input;
            Console.Clear();
            if (File.Exists(filePath))
            {
                Console.WriteLine("Current employee list:");
                foreach (var emp in employees)
                {
                    Console.WriteLine($"{emp.Id}. {emp.FirstName} {emp.Surname}");
                }

                do
                {
                    Console.Write($"If you want to remove employee use number > 0 and max:" +
                    $" {employees.Count()}\nPlease write number to delete employee: ");
                    input = Console.ReadLine();

                    valid = CheckData.CheckingIntData(input);

                } while (!valid);

                numberToRemove = int.Parse(input);

                var employee = employees.FirstOrDefault(x => x.Id == numberToRemove);
                File.Delete(filePath);

                if (numberToRemove <= 0)
                {
                    Console.WriteLine($"Please use number > 0");
                }

                Console.Clear();

                using (var allActions = File.AppendText(actionsFile))
                {
                    allActions.Write($"{DateTime.Now}-EmployeeDeleted- Id:{employee.Id}, FirstName: {employee.FirstName}, Date: {DateTime.UtcNow}");
                }


                return (Employee)employee;
            }
            else
            {
                Console.WriteLine("You can't delete the employee - file doesn't exist ! Please choose another option from 'Main Menu' !\n\nPress any key to open 'Main Menu' !\n");
                Console.ReadKey();
            }
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
