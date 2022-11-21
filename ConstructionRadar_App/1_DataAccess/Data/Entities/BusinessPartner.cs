namespace ConstructionRadar_App.Entities
{
    public class BusinessPartner : EntityBase
    {

        public string? Name { get; set; }
        public override string ToString() => $"Id: {Id}, FirstName: {Name}";


    }
}
