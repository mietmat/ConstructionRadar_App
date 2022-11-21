namespace ConstructionRadar_App.Entities
{
    public class Employee : EntityBase
    {
        public string? FirstName { get; set; }
        public string? Surname { get; set; }
        public string? CompanyName { get; set; }
        public decimal Salary { get; set; }

        public override string ToString() => $"Id: {Id}, FirstName: {FirstName}, Company: {CompanyName}, Salary: {Salary}";


    }
}
