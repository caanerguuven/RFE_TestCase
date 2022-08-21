using RFECase.Domain.DTO;
using RFECase.Domain.DTO.Base;
using System.Threading.Tasks;

namespace RFECase.Service.Abstract
{
    public interface IDiffService
    {
        Task<DiffResponseDTO> GetDiff(int id);
        Task<BaseResponseDTO> SendToLeft(int id,string input);
        Task<BaseResponseDTO> SendToRight(int id, string input);
    }
}
