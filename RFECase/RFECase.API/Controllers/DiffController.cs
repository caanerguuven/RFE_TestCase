using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RFECase.API.ViewModel;
using RFECase.Service.Abstract;
using System.Threading.Tasks;

namespace RFECase.API.Controllers
{
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class DiffController : ControllerBase
    {
        private readonly IDiffService _diffService;

        public DiffController(IDiffService diffService)
        {
            _diffService = diffService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDiff(int id)
        {
            var result = await _diffService.GetDiff(id);
            return new JsonResult(result);
        }

        [HttpPost("{id}/left")]
        public async Task<IActionResult> SendToLeft(int id, [FromBody] DiffRequestViewModel model)
        {
            var result = await _diffService.SendToLeft(id, model.Input);
            return new JsonResult(result);
        }

        [HttpPost("{id}/right")]
        public async Task<IActionResult> SendToRight(int id, [FromBody] DiffRequestViewModel model)
        {
            var result = await _diffService.SendToRight(id, model.Input);
            return new JsonResult(result);
        }
    }
}
