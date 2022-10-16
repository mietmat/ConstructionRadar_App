using ConstructionRadar_App.Entities;

namespace ConstructionRadar_App.Components.TxtReader
{
    public class TxtReader : ITxtReader
    {
        public List<Employee> ProcessEmployee(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new List<Employee>();
            }
            else
            {
                var employees =
                File.ReadAllLines(filePath)
                .Where(x => x.Length > 1)
                .Select(x =>
                {
                    var columns = x.Split(' ');

                    return new Employee()
                    {
                        Id = int.Parse(columns[0]),
                        FirstName = columns[1],
                        Surname = columns[2]
                    };
                });

                if (employees.ToList().Count() > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Current list with employees: ");
                    Console.ForegroundColor = ConsoleColor.White;

                    foreach (var employee in employees)
                    {
                        Console.WriteLine($"{employee.Id}. {employee.FirstName} {employee.Surname}");
                    }
                }
                else
                {
                    Console.WriteLine("Empty list of employee. Please add new employ !");
                }

                return employees.ToList();

            }




        }
    }
}
