using PontoAll.WebAPI.Objects.Dtos.Entities;
using System.Globalization;

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

            // Verifica se a data construída com Day e YearMonth é válida
            string fullDate = $"{scale.YearMonth}-{scale.Day:D2}";
            if (!DateTime.TryParseExact(fullDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
                throw new Exception("Data completa inválida.");

            // Validação do dia da semana (DayType)
            if (scale.DayType < 1 || scale.DayType > 7)
                throw new Exception("Dia da semana inválido.");

            string diaSemana = CultureInfo.GetCultureInfo("pt-BR").DateTimeFormat.GetDayName(parsedDate.DayOfWeek);
            string nomeMes = CultureInfo.GetCultureInfo("pt-BR").DateTimeFormat.GetMonthName(parsedDate.Month);

            Console.WriteLine($"Dia da semana: {diaSemana}, Mês: {nomeMes}");

            var pickProperties = typeof(ScaleDTO).GetProperties()
                .Where(p => p.Name.StartsWith("Pick"))
                .OrderBy(p => p.Name)
                .ToList();

            TimeOnly? previous = null;
            foreach (var prop in pickProperties)
            {
                var current = (TimeOnly?)prop.GetValue(scale);

                if (current.HasValue)
                {
                    // Validação de faixa de horário: entre 00:00:00 e 23:59:59
                    if (current.Value < TimeOnly.MinValue || current.Value > new TimeOnly(23, 59, 59))
                        throw new Exception($"{prop.Name} contém horário inválido. Use valores entre 00:00:00 e 23:59:59.");

                    // Verificação se o horário está em ordem crescente
                    if (previous.HasValue && current < previous)
                        throw new Exception($"{prop.Name} deve ser maior que o horário anterior.");

                    previous = current;
                }
            }
        }
        private static bool IsValidMonth(string yearMonth)
        {
            return DateTime.TryParseExact(yearMonth, "yyyy-MM", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out _);
        }
    }
}
