using System.Globalization;

namespace MyPortfolio.Utility
{
    public static class GenericUtils
    {
        public static DateTime ConvertStringToDateTime(string dateTime)
        {
            return DateTime.ParseExact(dateTime, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        }

        public static DateTime ConvertSringTodateTimeWithFormat(string dateTime, string format)
        {
            return DateTime.ParseExact(dateTime, format, CultureInfo.InvariantCulture);
        }
    }
}
