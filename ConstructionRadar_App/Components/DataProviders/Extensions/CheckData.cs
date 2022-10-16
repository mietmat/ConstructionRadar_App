namespace ConstructionRadar_App.Components.DataProviders.Extensions
{
    public static class CheckData
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

        public static bool CheckingIntData(string data)
        {
            bool checking = true;
            int value;

            if (int.TryParse(data.ToString(), out value))
            {
                return checking;
            }

            return checking = false;


        }
    }
}
