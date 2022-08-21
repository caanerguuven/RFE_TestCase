using Newtonsoft.Json;
using RFECase.Domain.DTO;
using RFECase.Domain.DTO.Base;
using RFECase.UIConsole.DTO;
using RFECase.UIConsole.Handler;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RFECase.UIConsole
{
    public class RFEHandler : IRFEHandler
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly string baseUrl = "https://localhost:44351/v1";


        public RFEHandler(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public async Task<string> SendToLeft(int id, string input)
        {
            var url = $"{baseUrl}/diff/{id}/left";
            var request = new DiffRequestDTO { Input = input };

            var requestJson = JsonConvert.SerializeObject(request);
            var data = new StringContent(requestJson, Encoding.UTF8, "application/json");

            var response = await _clientFactory.CreateClient().PostAsync(url, data);
            var result = await response.Content.ReadAsStringAsync();
            var jsonResult = JsonConvert.DeserializeObject<BaseResponseDTO>(result);

            return jsonResult.Result;
        }

        public async Task<string> SendToRight(int id, string input)
        {
            var url = $"{baseUrl}/diff/{id}/right";
            var request = new DiffRequestDTO { Input = input };

            var requestJson = JsonConvert.SerializeObject(request);
            var data = new StringContent(requestJson, Encoding.UTF8, "application/json");

            var response = await _clientFactory.CreateClient().PostAsync(url, data);
            var result = await response.Content.ReadAsStringAsync();
            var jsonResult = JsonConvert.DeserializeObject<BaseResponseDTO>(result);

            return jsonResult.Result;
        }

        public async Task<string> GetDiff(int id)
        {
            var url = $"{baseUrl}/diff/{id}";

            var response = await _clientFactory.CreateClient().GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            var jsonResult = JsonConvert.DeserializeObject<DiffResponseDTO>(result);

            return jsonResult.Detail != null ? result : jsonResult.Result;
        }
    }
}
