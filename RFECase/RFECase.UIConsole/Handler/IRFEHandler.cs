using System.Threading.Tasks;

namespace RFECase.UIConsole.Handler
{
    public interface IRFEHandler
    {
        Task<string> SendToLeft(int id, string input);
        Task<string> SendToRight(int id, string input);
        Task<string> GetDiff(int id);
    }
}
