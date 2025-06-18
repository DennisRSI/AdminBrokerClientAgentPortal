namespace Codes1.Service.ViewModels
{
    public class CardDistributionViewModel : _BaseViewModel
    {
        public string Name { get; set; }
        public int PhysicalTotal { get; set; }
        public int PhysicalAvailableActivations { get; set; }
        public int PhysicalActivated { get; set; }
        public int VirtualTotal { get; set; }
        public int VirtualAvailableActivations { get; set; }
        public int VirtualActivated { get; set; }

        public string PhysicalRate
        {
            get
            {
                //return GetRate(PhysicalAvailableActivations, PhysicalActivated);
                return GetRate(PhysicalAvailableActivations, PhysicalActivated);
            }
        }

        public string VirtualRate
        {
            get
            {
                //return GetRate(VirtualAvailableActivations, VirtualActivated);
                return GetRate(VirtualAvailableActivations, VirtualActivated);
            }
        }

        private string GetRate(int total, int activated)
        {
            if (total == 0)
            {
                return "Unlimited";
            }



            var percentage = (decimal)activated / (decimal)total;
            percentage *= 100;

            return percentage.ToString("0.0") + "%";
        }
    }
}
