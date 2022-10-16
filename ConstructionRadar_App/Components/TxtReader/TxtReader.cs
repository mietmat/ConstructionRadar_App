using ConstructionRadar_App.Entities;

namespace ConstructionRadar_App.Components.TxtReader
{
    public class TxtReader : ITxtReader
    {
        public List<Employee> ReadEmployeesFromFile(string filePath)
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

                return employees.ToList();

            }




        }
    }
}
