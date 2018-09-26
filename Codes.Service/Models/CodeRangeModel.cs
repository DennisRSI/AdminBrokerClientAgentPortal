using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codes.Service.Models
{
    [Table("CodeRanges")]
    public class CodeRangeModel : _BaseModel
    {
        [Key, Required]
        public int CodeRangeId { get; set; }

        [Required]
        public int RSIOrganizationId { get; set; }

        [StringLength(50)]
        public string PreAlphaCharacters { get; set; }

        [StringLength(50)]
        public string PostAlphaCharacters { get; set; }

        [Required]
        public int StartNumber { get; set; }

        [Required]
        public int EndNumber { get; set; }

        [Required]
        public int IncrementByNumber { get; set; } = 1;

        [Required]
        public int Padding { get; set; } = 5;

        [Required, StringLength(50)]
        public string CodeType { get; set; } = "Virtual";

        public DateTime? DeactivationReason { get; set; } = null;

        [Required]
        public float Points { get; set; } = 0;

        [Required]
        public decimal Cost { get; set; } = 0;

        [Required]
        public int NumberOfUses { get; set; } = 0;

        [Required]
        public int BrokerId { get; set; }

        [ForeignKey("BrokerId")]
        public BrokerModel Broker { get; set; }

        public ICollection<CodeActivationModel> CodeActivations { get; set; }
        public ICollection<UnusedCodeModel> UnusedCodes { get; set; }
        public ICollection<UsedCodeModel> UsedCodes { get; set; }
        public ICollection<PendingCodeModel> PendingCodes { get; set; }
    }

    public static class CodeRangeModelExtensions
    {
        public static int GetTotalCodes(this CodeRangeModel model)
        {
            var totalCodes = (model.EndNumber - model.StartNumber) / model.IncrementByNumber;
            return ++totalCodes;
        }

        public static int GetTotalPossibleActivations(this CodeRangeModel model)
        {
            return model.GetTotalCodes() * model.NumberOfUses;
        }
    }

    public static class CodeType
    {
        public const string Virtual = "Virtual";
        public const string Physical = "Physical";
    }
}
