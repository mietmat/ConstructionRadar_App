using ConstructionRadar_App.Components.TxtReader;
using ConstructionRadar_App.Data;
using ConstructionRadar_App.Entities;
using ConstructionRadar_App.Repositories;
using ConstructionRadar_App.UI;

namespace ConstructionRadar_App
{
    public class App : IApp
    {
        private readonly IRepository<Employee> _employeesRepository;
        private readonly ITxtReader _txtReader;
        private readonly IUserCommunication _userCommunication;
        private readonly ConstructionRadarDbContext _constructionRadarDbContext;


        public App(IRepository<Employee> employeeRepository, ITxtReader txtReader, IUserCommunication userCommunication, ConstructionRadarDbContext constructionRadarDbContext)
        {
            _employeesRepository = employeeRepository;
            _txtReader = txtReader;
            _constructionRadarDbContext = constructionRadarDbContext;
            _userCommunication = userCommunication;
            _constructionRadarDbContext.Database.EnsureCreated();//make sure DataBase existing / creating DataBase
        }
        public void Run()
        {          
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.White;

                UI_Welcome.AppDescription();
                Console.Write("Please select one option of the 'Main Menu' by clicking assigned number or Q to Quit: ");
                var input = Console.ReadLine();

                if (input.ToString() == "q")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nThanks for using Construction Radar ! See you soon !\nPress any key to close the console view !");
                    Console.ForegroundColor = ConsoleColor.White;

                    break;
                }

                try
                {
                    int temp;
                    if (int.TryParse(input.ToString(), out temp))
                    {
                        UI_Welcome.MainMenuNumber = temp;
                        _userCommunication.SelectedNumber(temp);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        throw new ArgumentException($"Invalid argument: {nameof(input)}. Please use only numbers!");

                    }
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

        }


        public void Close()
        {
            Console.WriteLine("See you again !");

        }
    }
}
