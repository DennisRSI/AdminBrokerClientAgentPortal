using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codes.Service.Models
{
    [Table("Videos")]
    public class VideoModel : _BaseModel
    {
        [Key, Required]
        public int VideoId { get; set; }

        [StringLength(2000), Required]
        public string Description { get; set; }

        [Required]
        public bool IsPreLogin { get; set; }

        [StringLength(2000), Required]
        public string Url { get; set; }
    }
}
