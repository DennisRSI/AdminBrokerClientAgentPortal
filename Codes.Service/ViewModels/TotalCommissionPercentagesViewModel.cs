namespace Codes.Service.ViewModels
{
    public class TotalCommissionPercentagesViewModel
    {
        public float TotalCommissionPercentage { get; set; } = 0;
        public float TotalAgentCommissionPercentage { get; set; } = 0;
        public float TotalClientCommissionPercentage { get; set; } = 0;
        public float TotalCommissionUsed { get { return TotalAgentCommissionPercentage + TotalClientCommissionPercentage; } }
        public float TotalBrokerCommissionPercentage { get { return TotalCommissionPercentage - TotalCommissionUsed; } }
        public string Message { get; set; } = "";
        public bool IsSuccess { get; set; } = false;
    }
}
