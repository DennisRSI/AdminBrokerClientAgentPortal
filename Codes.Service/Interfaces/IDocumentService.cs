using Codes.Service.Models;

namespace Codes.Service.Interfaces
{
    public interface IDocumentService
    {
        DocumentModel Get(int id);
        void Add(string role, int userId, DocumentType type, string contentType, byte[] data);
    }
}
