namespace ConstructionRadar_App.UI
{
    public interface IUserCommunication
    {
        void SelectedNumber(int number);
        void EnterEmployeeName();
        void EnterEmployeeSurname();
        void AddEmployeeToFile();
        void DeleteEmployeeFromFile();

    }
}
