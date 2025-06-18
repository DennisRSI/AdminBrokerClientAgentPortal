using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codes1.Service.Models
{
    [Table("Documents")]
    public class DocumentModel : _BaseModel
    {
        [Key, Required]
        public int DocumentId { get; set; }

        [Required]
        public DocumentType DocumentType { get; set; }

        [Required]
        public string ContentType { get; set; }

        [Required]
        public byte[] Data { get; set; }
    }

    public enum DocumentType
    {
        Unknown = 0,
        W9 = 1,
    }
}
