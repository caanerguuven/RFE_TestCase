using System.Text.Json.Serialization;

namespace RFECase.Domain.DTO.Base
{
    public class BaseResponseDTO
    {
        [JsonPropertyName("statusCode")]
        public int StatusCode { get; set; }
        [JsonPropertyName("result")]
        public string Result { get; set; }
    }
}
