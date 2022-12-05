
namespace ConstructionRadar_App
{
    public class UI_Welcome
    {
        public static int MainMenuNumber { get; set; }
        public static void AppDescription()
        {
            Console.WriteLine("*****CONSTRUCTION RADAR*****");
            Console.WriteLine();
            Console.WriteLine("Welcome in Construction Radar Application !\nOur application made for Civil Engineers or Construction Contractor to build the future !\n" +
                "You can manage your employees and contracts here !\nThe building process is faster with us !\n");

            Console.WriteLine("Main Menu\n\n" +
                "1.  Show current Employees\n" +
                "2.  Add new Employee\n" +
                "3.  Delete Employee\n" +
                "4.  Edit Employee\n" +
                "5.  Show current Contracts\n" +
                "6.  Add new Contract\n" +
                "7.  Delete Contract\n" +
                "8.  Edit Contract\n"+
                "9.  Show Data");
            //"9.  Add Equipment\n" +
            //"10. Delete Equipment\n" +                
            //"11. Edit Equipment\n" +                
            //"12. Add new Subcontractor\n" +
            //"13. Delete Subcontractor\n" +
            //"14. Edit Subcontractor\n");

            Console.WriteLine("If you want to quit press Q.\n");

        }
    }
}
