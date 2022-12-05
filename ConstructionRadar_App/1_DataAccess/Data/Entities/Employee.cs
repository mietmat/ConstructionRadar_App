using System.Runtime.CompilerServices;
using System.Text;

namespace ConstructionRadar_App.Entities
{
    public class Employee : EntityBase
    {
        public string? FirstName { get; set; }
        public string? Surname { get; set; }
        public string? CompanyName { get; set; }
        public decimal Salary { get; set; }
        public Function Function { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new();
            sb.AppendLine($"1.FirstName: {FirstName}\n2.SurName: {Surname}\n3.Company: {CompanyName}\n4.Salary: {Salary}\n5.Function: {Function}");

            return sb.ToString();
        }  

    }
}
