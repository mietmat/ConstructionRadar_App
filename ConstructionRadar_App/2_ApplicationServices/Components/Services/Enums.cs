namespace ConstructionRadar_App
{
    public enum EmployeeProperty
    {
        FirstName = 1,
        SurName,
        CompanyName,
        Salary,
        Function
    }

    public enum ContractProperty
    {
        Name = 1,
        Country,
        City,
        Budget,
        StartDate,
        FinishDate
    }

    public enum Function
    {
        ProjectManager = 1,
        TeamLeader,        
        Engineer,
        Worker
    }

    public enum Roles
    {
        User = 1,
        Admin        
    }
}
