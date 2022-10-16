using ConstructionRadar_App.Components.TxtReader;
using ConstructionRadar_App.Entities;

namespace ConstructionRadar_App.UI
{
    public class UserCommunication : IUserCommunication
    {
        public ITxtReader _txtReader { get; set; }
        Employee employee = new();

        string filePath = "Employees.txt";
        string actionsFile = $"AllAction.txt";
        string name = "";
        string surname = "";

        public UserCommunication(ITxtReader txtReader)
        {
            _txtReader = txtReader;
        }

        public void AddEmployeeToFile()
        {
            if (!File.Exists(filePath))
                using (var allEmployee = File.Create(filePath))
                {
                }
            Console.Clear();
            var lines = _txtReader.ProcessEmployee(filePath);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"We have {lines.Count() + 1} employees !");
            Console.ForegroundColor = ConsoleColor.White;

            using (var allEmployee = File.AppendText(filePath))
            {
                allEmployee.WriteLine($"{lines.Count() + 1} {name} {surname}");

                Console.WriteLine($"Added new employee: {name}!");
            }

            using (var allActions = File.AppendText(actionsFile))
            {
                allActions.WriteLine($"{DateTime.UtcNow}-EmployeeAdded- {lines.Count() + 1}. {name} {surname}");
            }

            _txtReader.ProcessEmployee(filePath);

            Console.WriteLine("Press any key to open 'Main Menu' !");
            Console.ReadKey();
        }

        public void DeleteEmployeeFromFile()
        {
            Console.Clear();
            if (File.Exists(filePath))
            {
                IEnumerable<Employee> lines = _txtReader.ProcessEmployee(filePath);

                Console.Write($"If you want to remove employee use number > 0 and max: {lines.Count()}\nPlease write number to delete employee: ");


                int numberToRemove = int.Parse(Console.ReadLine());

                File.Delete(filePath);

                if (numberToRemove <= 0)
                {
                    Console.WriteLine($"Please use number > 0");
                }

                Console.Clear();
                var list = lines.ToList();

                using (var allActions = File.AppendText(actionsFile))
                {
                    allActions.Write($"{DateTime.Now}-EmployeeDeleted- {list.ElementAt(numberToRemove - 1)}");
                }

                list.RemoveAt(numberToRemove - 1);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("List after employee remove: ");
                Console.ForegroundColor = ConsoleColor.White;

                var id = 0;
                foreach (var itemAfterRemove in list)
                {
                    itemAfterRemove.Id = ++id;
                    if (itemAfterRemove != null)
                    {
                        Console.Write(itemAfterRemove);
                    }

                    using (var afterRemove = File.AppendText(filePath))
                    {
                        if (afterRemove != null)
                        {
                            afterRemove.Write($"{itemAfterRemove}");

                        }
                    }
                }

            }
            else
            {
                Console.WriteLine("You can't delete the employee - file doesn't exist ! Please choose another option from 'Main Menu' !\n\nPress any key to open 'Main Menu' !\n");
                Console.ReadKey();
            }

            Console.WriteLine("Press any key to open 'Main Menu' !");
            Console.ReadKey();
            Console.Clear();
        }

        public void EnterEmployeeName()
        {
            Console.Write("Please enter name of the employee: ");

            employee.FirstName = name;
            name = Console.ReadLine().ToUpper();

            while (!CheckingStringData(name))
            {
                Console.Write("Employee name should have only a letters ! Please enter name of the employee: ");
                name = Console.ReadLine().ToUpper();
            }

            if (name.ToLower() == "q")
            {
                Environment.Exit(0);
            }
        }

        public void EnterEmployeeSurname()
        {
            Console.Write("Please enter surname of the employee: ");

            employee.Surname = surname;
            surname = Console.ReadLine().ToUpper();

            while (!CheckingStringData(surname))
            {
                Console.Write("Employee surname should have only a letters ! Please enter surname of the employee: ");
                surname = Console.ReadLine().ToUpper();
            }

            if (surname.ToLower() == "q")
            {
                Environment.Exit(0);
            }
        }


        public void SelectedNumber(int number)
        {
            switch (number)
            {
                case 0:
                    Console.Clear();
                    Console.WriteLine($"Please select number 1-14\nPress any key to open 'Main Menu'");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case 1:
                    Console.Clear();
                    _txtReader.ProcessEmployee(filePath);
                    EnterEmployeeName();
                    EnterEmployeeSurname();
                    AddEmployeeToFile();
                    Console.Clear();
                    break;
                case 2:
                    DeleteEmployeeFromFile();
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

        public bool CheckingStringData(string data)
        {
            bool checking = true;
            foreach (var number in data)
            {
                if (!char.IsDigit(number) && char.IsLetterOrDigit(number))
                {
                    return checking;
                }
                else
                    return checking = false;
            }

            return checking;

        }
    }
}
