using Codes1.Service.Models;

namespace Codes1.Service.Interfaces
{
    public interface IDocument1Service
    {
        DocumentModel Get(int id);
        void Add(string role, int userId, DocumentType type, string contentType, byte[] data);
    }
}
