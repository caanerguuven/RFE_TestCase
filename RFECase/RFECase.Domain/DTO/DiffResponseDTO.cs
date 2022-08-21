using RFECase.Domain.DTO.Base;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RFECase.Domain.DTO
{
    public class DiffResponseDTO:BaseResponseDTO
    {
        [JsonPropertyName("detail")]
        public DiffResponseDetailDTO Detail { get; set; }
        [JsonPropertyName("elaborations")]
        public List<DiffResponseElaborationDTO> Elaborations { get; set; }

        public DiffResponseDTO()
        {
            Elaborations = new List<DiffResponseElaborationDTO>();
        }
}
}
