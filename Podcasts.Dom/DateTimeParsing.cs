using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Podcasts.Dom
{
    internal static class DateTimeParsing
    {
        public static DateTime? TryParseDateTime(string value)
        {
            DateTime result;
            if(DateTime.TryParse(value, out result))
            {
                return result.ToUniversalTime();
            }

            value = 
                value.TrimEnd(' ', '\t')
                     .Replace("EDT", "-0400")
                     .Replace("EST", "-0500")
                     .Replace("CDT", "-0500")
                     .Replace("CST", "-0600")
                     .Replace("MDT", "-0600")
                     .Replace("MST", "-0700")
                     .Replace("PDT", "-0700")
                     .Replace("PST", "-0800");

            if(DateTime.TryParse(value, out result))
            {
                return result.ToUniversalTime();
            }
            else
            {
                return null;
            }
        }
    }
}
