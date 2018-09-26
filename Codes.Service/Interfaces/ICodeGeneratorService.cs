using Codes.Service.Domain;

namespace Codes.Service.Interfaces
{
    public interface ICodeGeneratorService
    {
        void GenerateCodes(CodeGeneratorOptions options);
    }
}
