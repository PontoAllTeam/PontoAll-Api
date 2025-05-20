using PontoAll.WebAPI.Objects.Dtos.Entities;
using System.Globalization;
using System.Linq;

namespace PontoAll.WebAPI.Objects.Utils
{
    public static class ScaleValidator
    {
        public static void Validate(ScaleDTO scale)
        {
            if (scale.Day < 1 || scale.Day > 31)
                throw new Exception("Dia inválido.");

            if (!IsValidMonth(scale.YearMonth))
                throw new Exception("Ano/mês inválido.");

            // Ajusta o separador para hífen para garantir o parse da data completa
            string yearMonthNormalized = scale.YearMonth.Replace('/', '-');
            string fullDate = $"{yearMonthNormalized}-{scale.Day:D2}";

            if (!DateTime.TryParseExact(fullDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
                throw new Exception("Data completa inválida.");

            // Validação do dia da semana (DayType)
            if (scale.DayType < 1 || scale.DayType > 7)
                throw new Exception("Dia da semana inválido.");

            string diaSemana = CultureInfo.GetCultureInfo("pt-BR").DateTimeFormat.GetDayName(parsedDate.DayOfWeek);
            string nomeMes = CultureInfo.GetCultureInfo("pt-BR").DateTimeFormat.GetMonthName(parsedDate.Month);

            // Console.WriteLine pode ser removido ou mantido para debug
            Console.WriteLine($"Dia da semana: {diaSemana}, Mês: {nomeMes}");

            var pickProperties = typeof(ScaleDTO).GetProperties()
                .Where(p => p.Name.StartsWith("Pick"))
                .OrderBy(p => p.Name)
                .ToList();

            TimeOnly? previous = null;
            foreach (var prop in pickProperties)
            {
                var value = prop.GetValue(scale) as string;

                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (!TimeOnly.TryParse(value, out var current))
                        throw new Exception($"{prop.Name} contém um horário inválido. Use o formato HH:mm:ss.");

                    if (current < TimeOnly.MinValue || current > new TimeOnly(23, 59, 59))
                        throw new Exception($"{prop.Name} contém horário fora do intervalo permitido.");

                    if (previous.HasValue && current < previous)
                        throw new Exception($"{prop.Name} deve ser maior que o horário anterior.");

                    previous = current;
                }
            }
        }
        private static bool IsValidMonth(string yearMonth)
        {
            string[] formats = { "yyyy-MM", "yyyy/MM" };
            return DateTime.TryParseExact(yearMonth, formats, CultureInfo.InvariantCulture,
                DateTimeStyles.None, out _);
        }
    }
}
