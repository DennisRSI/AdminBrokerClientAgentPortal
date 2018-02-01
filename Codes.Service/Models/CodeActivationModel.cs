using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Codes.Service.Models
{
    [Table("CodeActivations")]
    public class CodeActivationModel : _BaseModel
    {
        [Key, Required]
        public int CodeActivationId { get; set; }
        [Required]
        public int RSIId { get; set; } = 0;
        public DateTime? EmailVerifiedDate { get; set; } = null;
        [Required, StringLength(100)]
        public string Code { get; set; }
        [Required, StringLength(255)]
        public string FirstName { get; set; }
        [Required, StringLength(255)]
        public string MiddleName { get; set; }
        [Required, StringLength(255)]
        public string LastName { get; set; }
        [StringLength(50)]
        public string Phone1 { get; set; }
        [StringLength(50)]
        public string Phone2 { get; set; }
        [Required, StringLength(100)]
        public string Email { get; set; }
        [StringLength(255)]
        public string Address1 { get; set; }
        [StringLength(255)]
        public string Address2 { get; set; }
        [StringLength(100)]
        public string City { get; set; }
        [StringLength(100)]
        public string State { get; set; }
        [StringLength(50)]
        public string PostalCode { get; set; }
        [StringLength(100)]
        public string Country { get; set; }
        [Required, StringLength(100)]
        public string Username { get; set; }
        [Required, StringLength(100)]
        public string Password { get; set; }
        [Required]
        public decimal Paid { get; set; } = 0;

        /*[Required]
        public int CodeRangeId { get; set; }
        [ForeignKey("CodeRangeId")]
        public CodeRangeModel CodeRange { get; set; }*/
    }
}
