using System.Text;

namespace ConstructionRadar_App.Entities
{
    public class Contract : EntityBase
    {
        public string? Name { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public decimal Budget { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
             
        public override string ToString()
        {
            StringBuilder sb = new();
            sb.AppendLine($"1.Name: {Name}\n" +
                $"2.Country: {Country}\n" +
                $"3.City: {City}\n" +
                $"4.Budget: {Budget}\n" +
                $"5.Start date: {StartDate}\n" +
                $"6.Finish date: {FinishDate}");

            return sb.ToString();
        }


    }
}
