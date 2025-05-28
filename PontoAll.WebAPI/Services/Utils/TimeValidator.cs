using PontoAll.WebAPI.Objects.Dtos.Entities;
using System;

namespace PontoAll.WebAPI.Services.Utils
{
    public static class TimeValidator
    {
        public static void ValidateTime(ScaleDTO scaleDTO)
        {
            var pickProperties = typeof(ScaleDTO).GetProperties()
                .Where(p => p.Name.StartsWith("Pick"))
                .OrderBy(p => p.Name)
                .ToList();

            TimeOnly? previous = null;

            foreach (var prop in pickProperties)
            {
                var value = prop.GetValue(scaleDTO) as string;

                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (!TimeOnly.TryParse(value, out var current))
                    {
                        throw new Exception($"{prop.Name} contém um horário inválido. Use o formato HH:mm:ss.");
                    }
                    if (previous.HasValue && current < previous)
                    {
                        throw new Exception($"{prop.Name} deve ser maior ou igual ao horário anterior.");
                    }

                    previous = current;
                }
                else
                {
                    }
            }
        }
    }
}
