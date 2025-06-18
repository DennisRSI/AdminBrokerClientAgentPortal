using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codes1.Service.Models
{
    [Table("ClientAgents")]
    public class ClientAgentModel : _BaseModel
    {
        public ClientAgentModel()
        {
        }

        [Key, Required]
        public int ClientAgentId { get; set; }

        public int ClientId { get; set; }
        public ClientModel Client { get; set; }

        public int AgentId { get; set; }
        public AgentModel Agent { get; set; }

        public decimal CommissionRate { get; set; }
    }
}
