using System.Text.Json.Serialization;
using PontoAll.WebAPI.Objects.Utils;

namespace PontoAll.WebAPI.Objects.Dtos.Entities
{
    public class ScaleDTO
    {
        public int Id { get; set; }
        public int Day { get; set; }
        public string YearMonth { get; set; }
        public int DayType { get; set; }

        [JsonConverter(typeof(NullableTimeOnlyJsonConverter))]
        public TimeOnly? Pick1 { get; set; }

        [JsonConverter(typeof(NullableTimeOnlyJsonConverter))]
        public TimeOnly? Pick2 { get; set; }

        [JsonConverter(typeof(NullableTimeOnlyJsonConverter))]
        public TimeOnly? Pick3 { get; set; }

        [JsonConverter(typeof(NullableTimeOnlyJsonConverter))]
        public TimeOnly? Pick4 { get; set; }

        [JsonConverter(typeof(NullableTimeOnlyJsonConverter))]
        public TimeOnly? Pick5 { get; set; }

        [JsonConverter(typeof(NullableTimeOnlyJsonConverter))]
        public TimeOnly? Pick6 { get; set; }

        [JsonConverter(typeof(NullableTimeOnlyJsonConverter))]
        public TimeOnly? Pick7 { get; set; }

        [JsonConverter(typeof(NullableTimeOnlyJsonConverter))]
        public TimeOnly? Pick8 { get; set; }

        [JsonConverter(typeof(NullableTimeOnlyJsonConverter))]
        public TimeOnly? Pick9 { get; set; }

        [JsonConverter(typeof(NullableTimeOnlyJsonConverter))]
        public TimeOnly? Pick10 { get; set; }

        public int UserId { get; set; }
    }
}
