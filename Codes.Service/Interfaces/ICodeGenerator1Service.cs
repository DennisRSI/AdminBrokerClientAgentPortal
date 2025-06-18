using Codes1.Service.Domain;
using System.Threading.Tasks;

namespace Codes1.Service.Interfaces
{
    public interface ICodeGenerator1Service
    {
        void GenerateCodes(CodeGeneratorOptions options);

        Task<(bool isSuccess, string message)> CheckAvailableCodes(int startNumber, int numberOfCodes, int skipAmount, string preAlpha, string postAlpha = "", int padding = 1);
        
    }
}
