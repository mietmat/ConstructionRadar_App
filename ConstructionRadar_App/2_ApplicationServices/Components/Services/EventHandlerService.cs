using ConstructionRadar_App.Entities;
using ConstructionRadar_App.Entities.Extensions;
using ConstructionRadar_App.Repositories;

namespace ConstructionRadar_App.Services;

public class EventHandlerService : IEventHandlerService
{
    private readonly IRepository<Employee> _employeeRepository;
    private readonly IRepository<Contract> _contractRepository;
    public EventHandlerService(IRepository<Employee> employeeRepository, IRepository<Contract> contractRepository)
    {
        _employeeRepository = employeeRepository;
        _contractRepository = contractRepository;
    }

    public void SubscribeToEvents()
    {
        _employeeRepository.ItemAdded += EmployeeRepositoryOnItemAdded;
        _employeeRepository.ItemRemoved += EmployeeRepositoryOnItemRemoved;
        _employeeRepository.ItemUpdated += EmployeeRepositoryOnItemUpdated;
        _contractRepository.ItemAdded += ContractRepositoryOnItemAdded;
        _contractRepository.ItemRemoved += ContractRepositoryOnItemRemoved;
        _contractRepository.ItemUpdated += ContractRepositoryOnItemUpdated;
    }

    private void EmployeeRepositoryOnItemAdded(object? sender, Employee e)
    {
        AddAuditInfo(e, "EMPLOYEE ADDED");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"{e.GetType().Name}\n{e}added successfully.\n");
        Console.ResetColor();
    }

    private void EmployeeRepositoryOnItemRemoved(object? sender, Employee e)
    {
        AddAuditInfo(e, "EMPLOYEE REMOVED");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"{e.GetType().Name}\n{e}removed successfully.\n");
        Console.ResetColor();
    }

    private void EmployeeRepositoryOnItemUpdated(object? sender, Employee e)
    {
        AddAuditInfo(e, "EMPLOYEE UPDATED");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"{e.GetType().Name}\n{e}updated successfully.\n");
        Console.ResetColor();
    }

    private void ContractRepositoryOnItemAdded(object? sender, Contract e)
    {
        AddAuditInfo(e, "CONTRACT ADDED");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"{e.GetType().Name}\n{e}added successfully.\n");
        Console.ResetColor();
    }

    private void ContractRepositoryOnItemRemoved(object? sender, Contract e)
    {
        AddAuditInfo(e, "CONTRACT REMOVED");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"{e.GetType().Name}\n{e}removed successfully.\n");
        Console.ResetColor();
    }

    private void ContractRepositoryOnItemUpdated(object? sender, Contract e)
    {
        AddAuditInfo(e, "CONTRACT UPDATED");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"{e.GetType().Name}\n{e}updated successfully.\n");
        Console.ResetColor();
    }

    private void AddAuditInfo(Employee e, string info)
    {
        using (var writer = File.AppendText((IRepository<IEntity>.auditFileName)))
        {
            writer.WriteLine($"[{DateTime.UtcNow}]-{info}:{e.ToStringOneLine()}");
        }
    }
    private void AddAuditInfo(Contract e, string info)
    {
        using (var writer = File.AppendText((IRepository<IEntity>.auditFileName)))
        {
            writer.WriteLine($"[{DateTime.UtcNow}]-{info}:{e.ToStringOneLine()}");
        }
    }
}
