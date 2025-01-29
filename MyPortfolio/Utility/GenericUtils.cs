using System.Globalization;

namespace MyPortfolio.Utility
{
    public static class GenericUtils
    {
        public static DateTime ConvertStringToDateTime(string dateTime)
        {
            return DateTime.ParseExact(dateTime, "yyyyMMdd_HHmmss", CultureInfo.InvariantCulture);
        }

        public static string ConvertDateTimeToString(DateTime dateTime)
        {
            return dateTime.ToString("yyyyMMdd_HHmmss", CultureInfo.InvariantCulture);
        }
    }
}
