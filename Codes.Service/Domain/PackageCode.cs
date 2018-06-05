namespace Codes.Service.Domain
{
    public static class PackageCode
    {
        /// <summary>
        /// this method returns the package id based on this information:
        //  Hotels-> 1026
        //  Hotels, Condos-> 1080
        //  Hotels, Shopping->1081
        //  Hotels, Dining->1082
        //  Hotels, Condos, Shopping-> 1033
        //  Hotels, Condos, Dining->1083
        //  Hotels, Shopping Dining ->1084
        //  Hotels, Condos, Shopping, Dining->1085
        /// </summary>
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
    }
}
