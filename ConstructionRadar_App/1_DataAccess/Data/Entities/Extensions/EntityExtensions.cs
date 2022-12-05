using System.Text;
using System.Text.Json;

namespace ConstructionRadar_App.Entities.Extensions
{
    public static class EntityExtensions
    {
        public static T? Copy<T>(this T itemToCopy) where T : IEntity
        {
            var json = JsonSerializer.Serialize<T>(itemToCopy);
            return JsonSerializer.Deserialize<T>(json);
        }

        public static string ToStringOneLine(this Employee emp)
        {
            StringBuilder sb = new();
            sb.AppendLine($"{emp.FirstName}|{emp.Surname}|{emp.CompanyName}|{emp.Salary}");

            return sb.ToString();
        }

        public static string ToStringOneLine(this Contract contract)
        {
            StringBuilder sb = new();
            sb.AppendLine($"{contract.Name}|{contract.Country}|{contract.City}|" +
                $"{contract.Budget}|{contract.StartDate}|{contract.FinishDate}");

            return sb.ToString();
        }


    }
}
