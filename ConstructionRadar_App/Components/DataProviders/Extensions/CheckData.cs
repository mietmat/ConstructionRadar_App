namespace ConstructionRadar_App.Components.DataProviders.Extensions
{
    public class CheckData
    {
        public static bool CheckingStringData(string data)
        {
            bool checking = true;
            foreach (var number in data)
            {
                if (!(Char.IsDigit(number)) && Char.IsLetterOrDigit(number))
                {
                    return checking;
                }
                else
                    return checking = false;
            }

            return checking;

        }
    }
}
