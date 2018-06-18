﻿using System;
using System.Linq;

namespace Codes.Service.Domain
{
    public static class StringExtensions
    {
        public static string FormatPhone(this String phone)
        {
            if (String.IsNullOrWhiteSpace(phone))
            {
                return String.Empty;
            }

            // Remove everything except digits
            var digits = new string(phone.Where(c => char.IsDigit(c)).ToArray());

            if (digits.Length != 10)
            {
                return phone;
            }

            var part1 = digits.Substring(0, 3);
            var part2 = digits.Substring(3, 3);
            var part3 = digits.Substring(6, 4);

            return $"{part1}-{part2}-{part3}";
        }
    }
}
