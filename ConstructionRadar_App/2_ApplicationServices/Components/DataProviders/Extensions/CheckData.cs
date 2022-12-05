using System.Diagnostics.Contracts;

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

            return false;

        }
        public static bool CheckingDecimalData(string data)
        {
            bool checking = true;
            decimal value;

            if (decimal.TryParse(data.ToString(), out value))
            {
                return checking;
            }

            return false;
        }

        public static bool CheckingDateTimeData(string data)
        {
            bool checking = true;
            DateTime value;

            if (DateTime.TryParse(data.ToString(), out value))
            {
                return checking;
            }

            return false;
        }

        public static bool DateTimeValidation(DateTime startDate, DateTime finishDate)
        {
            bool checking = true;
            checking = startDate > finishDate ? false:true;            

            return checking;
        }

    }
}
