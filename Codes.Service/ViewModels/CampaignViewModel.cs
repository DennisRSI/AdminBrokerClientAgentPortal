using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Codes.Service.ViewModels
{
    public class CampaignViewModel : _BaseViewModel
    {
        [Display(Name = "Campaign Id", Prompt = "Campaign Id")]
        public int CampaignId { get; set; } = 0;

        [StringLength(100), Display(Name = "Campaign Name", Prompt = "Campaign Name")]
        public string CampaignName { get; set; }

        [StringLength(50), Display(Name = "Card Type", Prompt = "Card Type")]
        public string CampaignType { get; set; } = "Virtual";

        [Display(Name = "Card Quantity", Prompt = "Card Quantity")]
        public int CardQuantity { get; set; }

        public bool BenefitCondo { get; set; }
        public bool BenefitHotel { get; set; }
        public bool BenefitShopping { get; set; }

        [Display(Name = "Package Id", Prompt = "Package Id")]
        public int PackageId { get; set; }

        public DateTime? StartDateTime
        {
            get { return GetDateTime(StartDate, StartTime); }
            set { } // TODO
        }

        public DateTime? EndDateTime
        {
            get { return GetDateTime(EndDate, EndTime); }
            set { } // TODO
        }

        public string StartDate { get; set; }
        public string StartTime { get; set; }
        public string EndDate { get; set; }
        public string EndTime { get; set; }

        [Display(Name = "Campaign Description", Prompt = "Campaign Description")]
        public string CampaignDescription { get; set; }

        public int PreLoginVideoId { get; set; }

        public int PostLoginVideoId { get; set; }

        [Display(Name = "Google Analytics Code", Prompt = "Google Analytics Code")]
        public string GoogleAnalyticsCode { get; set; } = null;

        [Display(Name = "Custom CSS", Prompt = "Custom CSS")]
        public string CustomCSS { get; set; } = null;

        [Display(Name = "Deactivation Reason", Prompt = "Deactivation Reason")]
        public DateTime? DeactivationReason { get; set; } = null;

        [Display(Name = "Broker", Prompt = "Broker")]
        public BrokerViewModel Broker { get; set; } = new BrokerViewModel();

        [Display(Name = "Client", Prompt = "Client")]
        public ClientViewModel Client { get; set; } = new ClientViewModel();

        public string BenefitText
        {
            get
            {
                string result = String.Empty;

                result += BenefitCondo ? "Condo, " : String.Empty;
                result += BenefitHotel ? "Hotel, " : String.Empty;
                result += BenefitShopping ? "Shopping, " : String.Empty;

                return result.TrimEnd(',', ' ');
            }
        }

        public string StatusText
        {
            get
            {
                if (DeactivationDate != null)
                {
                    return "Inactive";
                }

                return "Active";
            }
        }

        private DateTime? GetDateTime(string date, string time)
        {
            string dateTime = $"{date} {time}";
            string format = "yyyy-MM-dd HH:mm";

            try
            {
                return DateTime.ParseExact(dateTime, format, CultureInfo.InvariantCulture);
            }
            catch (FormatException)
            {
                return null;
            }
        }
    }

    public class CampaignCodeRangeViewModel : _BaseViewModel
    {
        public int CampaignId { get; set; }
        public int CodeRangeId { get; set; }
    }

    public class CampaignAgentViewModel: _BaseViewModel
    {
        public int CampaignId { get; set; }
        public int AgentId { get; set; }
    }
}
