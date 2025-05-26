using PontoAll.WebAPI.Objects.Dtos.Entities;
using System.Globalization;

namespace PontoAll.WebAPI.Services.Utils
{
    public class DateValidator
    {
        public static bool IsValidMonth(string yearMonth)
        {
            string[] formats = { "yyyy-MM", "yyyy/MM" };
            return DateTime.TryParseExact(yearMonth, formats, CultureInfo.InvariantCulture,
                DateTimeStyles.None, out _);
        }
    }
}
