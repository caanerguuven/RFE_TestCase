using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace RFECase.Domain.DTO
{
    public class DiffResponseDetailDTO
    {
        [JsonPropertyName("diffId")]
        public int DiffID { get; set; }
        [JsonPropertyName("left")]
        public string LeftExpression { get; set; }
        [JsonPropertyName("leftLength")]
        public int LeftExpressionLength { get; set; }
        [JsonPropertyName("right")]
        public string RightExpression { get; set; }
        [JsonPropertyName("rightLength")]
        public int RightExpressionLength { get; set; }

    }
}
