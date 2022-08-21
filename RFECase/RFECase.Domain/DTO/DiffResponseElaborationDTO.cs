using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace RFECase.Domain.DTO
{
    public class DiffResponseElaborationDTO
    {
        [JsonPropertyName("index")]
        public int DiffIndex { get; set; }
        [JsonPropertyName("left")]
        public char LeftDiffValue { get; set; }
        [JsonPropertyName("right")]
        public char RightDiffValue { get; set; }
    }
}
