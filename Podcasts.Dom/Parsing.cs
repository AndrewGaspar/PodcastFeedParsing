using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Podcasts.Dom
{
    public static class Parsing
    {
        private static ulong? TryParseUlong(string value)
        {
            ulong result;
            if (ulong.TryParse(value, out result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        private static uint? TryParseUint(string value)
        {
            uint result;
            if (uint.TryParse(value, out result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        private static Uri TryParseUri(string value)
        {
            Uri result;
            if (Uri.TryCreate(value, UriKind.Absolute, out result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        private static bool? TryParseBool(string value)
        {
            bool result;
            if (bool.TryParse(value, out result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        private static DateTime? TryParseDateTime(string value) =>
            DateTimeParsing.TryParseDateTime(value);

        private static TimeSpan? TryParseTimeSpan(string value)
        {
            var maybeParts = value.Split(':').Select(TryParseUint).ToList();

            var allParsed = maybeParts.Aggregate(true, (isParsed, num) => isParsed && num.HasValue);
            if (!allParsed) return null;

            var parts = maybeParts.Select(num => (int)num.Value).ToList();

            // SS
            if (parts.Count == 1)
            {
                return new TimeSpan(hours: 0, minutes: 0, seconds: parts[0]);
            }

            // MM:SS
            if (parts.Count == 2)
            {
                return new TimeSpan(hours: 0, minutes: parts[0], seconds: parts[1]);
            }

            // HH:MM:SS
            if (parts.Count == 3)
            {
                return new TimeSpan(hours: parts[0], minutes: parts[1], seconds: parts[2]);
            }

            return null;
        }

        public static T TryParse<T>(string value)
        {
            if (value == null)
            {
                return default(T);
            }

            var type = typeof(T);

            if (type == typeof(string))
            {
                return (T)(object)value;
            }

            if (type == typeof(ulong?))
            {
                return (T)(object)TryParseUlong(value);
            }

            if (type == typeof(uint?))
            {
                return (T)(object)TryParseUint(value);
            }

            if (type == typeof(bool?))
            {
                return (T)(object)TryParseBool(value);
            }

            if (type == typeof(Uri))
            {
                return (T)(object)TryParseUri(value);
            }

            if (type == typeof(DateTime?))
            {
                return (T)(object)TryParseDateTime(value);
            }

            if (type == typeof(TimeSpan?))
            {
                return (T)(object)TryParseTimeSpan(value);
            }

            if (type == typeof(ExplicitEnum?))
            {
                return (T)(object)ExplicitEnumHelper.TryParseExplicitEnum(value);
            }

            if (type == typeof(YesEnum?))
            {
                return (T)(object)YesEnumHelper.TryParseYesEnum(value);
            }

            throw new InvalidOperationException($"{typeof(T)} is not supported for parsing.");
        }
    }
}
