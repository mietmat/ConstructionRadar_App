
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
                "You can manage your employees, equipment and subcontractors here !\nThe building process is faster with us !\n");

            Console.WriteLine("Main Menu\n\n" +
                "1.  Add new Employee\n" +
                "2.  Delete Employee\n" +
                "3.  Add new Manager\n" +
                "4.  Delete Manager\n" +
                "5.  Add new Business Parter\n" +
                "6.  Delete Business Partner\n" +
                "7.  Add Equipment\n" +
                "8.  Delete Equipment\n" +
                "9.  Add new Investment\n" +
                "10. Delete Investment\n" +
                "11. Add new Investor\n" +
                "12. Remove Investor\n" +
                "13. Add new Subcontractor\n" +
                "14. Delete Subcontractor\n");

            Console.WriteLine("If you want to quit press Q.\n");

        }
    }
}
