namespace Codes.Service.Domain
{
    /// <summary>
    /// This class uses this package information:
    //  Hotels-> 1026
    //  Hotels, Condos-> 1080
    //  Hotels, Shopping->1081
    //  Hotels, Dining->1082
    //  Hotels, Condos, Shopping-> 1033
    //  Hotels, Condos, Dining->1083
    //  Hotels, Shopping, Dining ->1084
    //  Hotels, Condos, Shopping, Dining->1085
    /// </summary>
    public static class PackageCode
    {
        public static int GetCode(bool condo, bool shopping, bool dining)
        {
            // There is probably a better way to do this, but this will work for now

            if (!condo && !shopping && !dining)
            {
                return 1026;
            }

            if (condo && !shopping && !dining)
            {
                return 1080;
            }

            if (!condo && shopping && !dining)
            {
                return 1081;
            }

            if (!condo && !shopping && dining)
            {
                return 1082;
            }

            if (condo && shopping && !dining)
            {
                return 1033;
            }

            if (condo && !shopping && dining)
            {
                return 1083;
            }

            if (!condo && shopping && dining)
            {
                return 1084;
            }

            return 1085;
        }

        public static string GetText(int packageId)
        {
            switch (packageId)
            {
                case 1026:
                    return "Hotel";

                case 1080:
                    return "Hotel, Condo";

                case 1081:
                    return "Hotel, Shopping";

                case 1082:
                    return "Hotel, Dining";

                case 1033:
                    return "Hotel, Condo, Shopping";

                case 1083:
                    return "Hotel, Condo, Dining";

                case 1084:
                    return "Hotel, Shopping, Dining";

                case 1085:
                    return "Hotel, Condo, Shopping, Dining";
            }

            return $"Unknown Package ID: {packageId}";
        }

        public static PackageBenefit GetBenefits(int packageId)
        {
            switch (packageId)
            {
                case 1026:
                    return new PackageBenefit(false, false, false);

                case 1080:
                    return new PackageBenefit(true, false, false);

                case 1081:
                    return new PackageBenefit(false, true, false);

                case 1082:
                    return new PackageBenefit(false, false, true);

                case 1033:
                    return new PackageBenefit(true, true, false);

                case 1083:
                    return new PackageBenefit(true, false, true);

                case 1084:
                    return new PackageBenefit(false, true, true);

                case 1085:
                    return new PackageBenefit(true, true, true);
            }

            return null;
        }
    }

    public class PackageBenefit
    {
        public PackageBenefit(bool condoBenefit, bool shoppingBenefit, bool diningBenefit)
        {
            CondoBenefit = condoBenefit;
            ShoppingBenefit = shoppingBenefit;
            DiningBenefit = diningBenefit;
        }

        public bool CondoBenefit { get; set; }
        public bool ShoppingBenefit { get; set; }
        public bool DiningBenefit { get; set; }
    }
}
