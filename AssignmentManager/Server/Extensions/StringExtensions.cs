﻿using System;
using System.Linq;

namespace AssignmentManager.Server.Extensions
{
    public static class StringExtensions
    {
        public static DateTime ParseToProjectTime(this string parsingSting)
        {
            string[] dateTimeFormats = {"yyyy-MM-dd HH:mm"};
            if (DateTime.TryParseExact(parsingSting,
                dateTimeFormats,
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None,
                out var dateTime))
            {
                Console.WriteLine("Date time converted: " + dateTime);
                return dateTime;
            }
            else
            {
                throw new Exception($"Can't convert string {parsingSting} to DateTime. Formats: {String.Join(", ", dateTimeFormats)}");
            }
        }
    }
}