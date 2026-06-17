using System.Text.Json.Serialization;
using static cts_twister_api.common.ResEnumerators;

namespace cts_twister_api.model
{
    public class MDResponse<T>
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ResultResponse Result { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
