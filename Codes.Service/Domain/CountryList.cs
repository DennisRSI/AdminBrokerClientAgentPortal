﻿using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Codes1.Service.Domain
{
    public static class CountryList
    {
        public static IEnumerable<SelectListItem> GetSelectList()
        {
            yield return new SelectListItem() { Text = "United States", Value = "USA", Selected = true };
            yield return new SelectListItem() { Text = "Canada", Value = "CAN" };
            yield return new SelectListItem() { Text = "----------------------------", Value = String.Empty };

            using (StringReader reader = new StringReader(_countryData))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    var split = line.Split(',');
                    var code = split[0].Trim();
                    var name = split[1].Trim();

                    yield return new SelectListItem() { Text = name, Value = code };
                }
            }
        }

        // Copied from here:
        // https://github.com/umpirsky/country-list/blob/master/data/en_US/country.csv
        // To match existing data:
        // US changed to USA
        // CA changed to CAN
        private static readonly string _countryData =
            @"  AF,Afghanistan
                AX,Åland Islands
                AL,Albania
                DZ, Algeria
                AS,American Samoa
                AD,Andorra
                AO, Angola
                AI,Anguilla
                AQ, Antarctica
                AG,Antigua & Barbuda
                AR,Argentina
                AM, Armenia
                AW,Aruba
                AC,Ascension Island
                AU,Australia
                AT, Austria
                AZ,Azerbaijan
                BS, Bahamas
                BH,Bahrain
                BD, Bangladesh
                BB,Barbados
                BY, Belarus
                BE,Belgium
                BZ, Belize
                BJ,Benin
                BM, Bermuda
                BT,Bhutan
                BO, Bolivia
                BA,Bosnia & Herzegovina
                BW,Botswana
                BR, Brazil
                IO,British Indian Ocean Territory
                VG,British Virgin Islands
                BN,Brunei
                BG, Bulgaria
                BF,Burkina Faso
                BI,Burundi
                KH, Cambodia
                CM,Cameroon
                CAN, Canada
                IC,Canary Islands
                CV,Cape Verde
                BQ,Caribbean Netherlands
                KY,Cayman Islands
                CF,Central African Republic
                EA,Ceuta & Melilla
                TD,Chad
                CL, Chile
                CN,China
                CX,Christmas Island
                CC,Cocos (Keeling) Islands
                CO,Colombia
                KM, Comoros
                CG,Congo - Brazzaville
                CD,Congo - Kinshasa
                CK,Cook Islands
                CR,Costa Rica
                CI,Côte d’Ivoire
                HR,Croatia
                CU, Cuba
                CW,Curaçao
                CY, Cyprus
                CZ,Czechia
                DK, Denmark
                DG,Diego Garcia
                DJ,Djibouti
                DM, Dominica
                DO,Dominican Republic
                EC,Ecuador
                EG, Egypt
                SV,El Salvador
                GQ,Equatorial Guinea
                ER,Eritrea
                EE, Estonia
                ET,Ethiopia
                EZ, Eurozone
                FK,Falkland Islands
                FO,Faroe Islands
                FJ,Fiji
                FI, Finland
                FR,France
                GF,French Guiana
                PF,French Polynesia
                TF,French Southern Territories
                GA,Gabon
                GM, Gambia
                GE,Georgia
                DE, Germany
                GH,Ghana
                GI, Gibraltar
                GR,Greece
                GL, Greenland
                GD,Grenada
                GP, Guadeloupe
                GU,Guam
                GT, Guatemala
                GG,Guernsey
                GN, Guinea
                GW,Guinea-Bissau
                GY, Guyana
                HT,Haiti
                HN, Honduras
                HK,Hong Kong SAR China
                HU,Hungary
                IS, Iceland
                IN,India
                ID, Indonesia
                IR,Iran
                IQ, Iraq
                IE,Ireland
                IM,Isle of Man
                IL,Israel
                IT, Italy
                JM,Jamaica
                JP, Japan
                JE,Jersey
                JO, Jordan
                KZ,Kazakhstan
                KE, Kenya
                KI,Kiribati
                XK, Kosovo
                KW,Kuwait
                KG, Kyrgyzstan
                LA,Laos
                LV, Latvia
                LB,Lebanon
                LS, Lesotho
                LR,Liberia
                LY, Libya
                LI,Liechtenstein
                LT, Lithuania
                LU,Luxembourg
                MO,Macau SAR China
                MK,Macedonia
                MG, Madagascar
                MW,Malawi
                MY, Malaysia
                MV,Maldives
                ML, Mali
                MT,Malta
                MH,Marshall Islands
                MQ,Martinique
                MR, Mauritania
                MU,Mauritius
                YT, Mayotte
                MX,Mexico
                FM, Micronesia
                MD,Moldova
                MC, Monaco
                MN,Mongolia
                ME, Montenegro
                MS,Montserrat
                MA, Morocco
                MZ,Mozambique
                MM,Myanmar (Burma)
                NA,Namibia
                NR, Nauru
                NP,Nepal
                NL, Netherlands
                NC,New Caledonia
                NZ,New Zealand
                NI,Nicaragua
                NE, Niger
                NG,Nigeria
                NU, Niue
                NF,Norfolk Island
                KP,North Korea
                MP,Northern Mariana Islands
                NO,Norway
                OM, Oman
                PK,Pakistan
                PW, Palau
                PS,Palestinian Territories
                PA,Panama
                PG,Papua New Guinea
                PY,Paraguay
                PE, Peru
                PH,Philippines
                PN,Pitcairn Islands
                PL,Poland
                PT, Portugal
                PR,Puerto Rico
                QA,Qatar
                RE, Réunion
                RO,Romania
                RU, Russia
                RW,Rwanda
                WS, Samoa
                SM,San Marino
                ST,São Tomé & Príncipe
                SA,Saudi Arabia
                SN,Senegal
                RS, Serbia
                SC,Seychelles
                SL,Sierra Leone
                SG,Singapore
                SX,Sint Maarten
                SK,Slovakia
                SI, Slovenia
                SB,Solomon Islands
                SO,Somalia
                ZA,South Africa
                GS,South Georgia & South Sandwich Islands
                KR,South Korea
                SS,South Sudan
                ES,Spain
                LK,Sri Lanka
                BL,St. Barthélemy
                SH,St. Helena
                KN,St. Kitts & Nevis
                LC,St. Lucia
                MF,St. Martin
                PM,St. Pierre & Miquelon
                VC,St. Vincent & Grenadines
                SD,Sudan
                SR, Suriname
                SJ,Svalbard & Jan Mayen
                SZ,Swaziland
                SE, Sweden
                CH,Switzerland
                SY, Syria
                TW,Taiwan
                TJ, Tajikistan
                TZ,Tanzania
                TH, Thailand
                TL,Timor-Leste
                TG, Togo
                TK,Tokelau
                TO, Tonga
                TT,Trinidad & Tobago
                TA,Tristan da Cunha
                TN,Tunisia
                TR, Turkey
                TM,Turkmenistan
                TC,Turks & Caicos Islands
                TV,Tuvalu
                UM,U.S. Outlying Islands
                VI,U.S. Virgin Islands
                UG,Uganda
                UA, Ukraine
                AE,United Arab Emirates
                GB,United Kingdom
                UN,United Nations
                USA,United States
                UY,Uruguay
                UZ, Uzbekistan
                VU,Vanuatu
                VA,Vatican City
                VE,Venezuela
                VN, Vietnam
                WF,Wallis & Futuna
                EH,Western Sahara
                YE,Yemen
                ZM, Zambia
                ZW,Zimbabwe";
    }
}
