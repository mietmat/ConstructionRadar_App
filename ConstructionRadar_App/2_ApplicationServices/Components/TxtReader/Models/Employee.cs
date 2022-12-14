using System.Text;

namespace MotoAppv2.Components.TxtReader.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Salary { get; set; }

        #region ToString Override
        public override string ToString()
        {
            StringBuilder sb = new(1024);
            sb.AppendLine($"{Id} {Name} {Surname}");
            return sb.ToString();
        }
        #endregion
    }
}
