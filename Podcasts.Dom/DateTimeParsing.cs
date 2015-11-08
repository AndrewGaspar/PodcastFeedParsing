using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Podcasts.Dom
{
    ///// <summary>
    ///// http://www.faqs.org/rfcs/rfc822.html
    ///// Section 5
    ///// </summary>
    //internal class RFC822DateTimeParsingStateMachine
    //{
    //    private static Dictionary<string, DayOfWeek> DaysOfWeek = new Dictionary<string, DayOfWeek>
    //    {
    //        { "Sun", DayOfWeek.Sunday },
    //        { "Mon", DayOfWeek.Monday },
    //        { "Tue", DayOfWeek.Tuesday },
    //        { "Wed", DayOfWeek.Wednesday },
    //        { "Thu", DayOfWeek.Thursday },
    //        { "Fri", DayOfWeek.Friday },
    //        { "Sat", DayOfWeek.Saturday },
    //    };

    //    private static Dictionary<string, int> MonthsOfYear = new Dictionary<string, int>
    //    {
    //        { "Jan", 1 },
    //        { "Feb", 2 },
    //        { "Mar", 3 },
    //        { "Apr", 4 },
    //        { "May", 5 },
    //        { "Jun", 6 },
    //        { "Jul", 7 },
    //        { "Aug", 8 },
    //        { "Sep", 9 },
    //        { "Oct", 10 },
    //        { "Nov", 11 },
    //        { "Dec", 12 },
    //    };

    //    private string PossibleDate;

    //    private char CurrentChar => PossibleDate[CurrentIndex];
    //    private int RemainingLength => PossibleDate.Length - CurrentIndex;
    //    private string CurrentString => PossibleDate.Substring(CurrentIndex);

    //    private int CurrentIndex;

    //    public RFC822DateTimeParsingStateMachine(string value)
    //    {
    //        PossibleDate = value;
    //        CurrentIndex = 0;
    //    }

    //    private bool AdvanceIfCharacter(char c)
    //    {
    //        if (RemainingLength <= 0)
    //        {
    //            return false;
    //        }

    //        if (CurrentChar == c)
    //        {
    //            CurrentIndex++;
    //            return true;
    //        }
    //        else
    //        {
    //            return false;
    //        }
    //    }

    //    private static readonly char[] WSP = new[] { ' ', '\t' };

    //    private bool AdvanceAtLeastOneSpace()
    //    {
    //        if(!AdvanceIfCharacter(' '))
    //        {
    //            return false;
    //        }

    //        while(AdvanceIfCharacter(' ')) { }

    //        return true;
    //    }

    //    private bool TryGetCharacters(int length, out string chars)
    //    {
    //        if(RemainingLength < length)
    //        {
    //            chars = null;
    //            return false;
    //        }

    //        chars = CurrentString.Substring(length);
    //        return true;
    //    }

    //    private bool TryGetNumDigits(int length, out string digits)
    //    {
    //        string possibleDigits;
    //        if(!TryGetCharacters(length, out possibleDigits))
    //        {
    //            digits = null;
    //            return false;
    //        }

    //        foreach(var possibleDigit in possibleDigits)
    //        {
    //            if(possibleDigit < '0' || possibleDigit > '9')
    //            {
    //                digits = null;
    //                return false;
    //            }
    //        }

    //        digits = possibleDigits;
    //        return true;
    //    }

    //    private bool TryReadDayOfWeek(out DayOfWeek result)
    //    {
    //        string chars;
    //        if(!TryGetCharacters(3, out chars))
    //        {
    //            result = DayOfWeek.Sunday;
    //            return false;
    //        }
            
    //        if (DaysOfWeek.TryGetValue(chars, out result))
    //        {
    //            CurrentIndex += 3;
    //            return true;
    //        }
    //        else
    //        {
    //            return false;
    //        }
    //    }

    //    private bool TryReadComma()
    //    {
    //        if(AdvanceIfCharacter(','))
    //        {
    //            return true;
    //        }
    //        else
    //        {
    //            return false;
    //        }
    //    }

    //    private bool TryReadDay(out DayOfWeek? result)
    //    {
    //        DayOfWeek dayOfWeek;
    //        if (TryReadDayOfWeek(out dayOfWeek))
    //        {
    //            // if there is a day of week at the beginning, it must be followed by a comma
    //            if (!TryReadComma())
    //            {
    //                result = null;
    //                return false;
    //            }
    //            else
    //            {
    //                result = dayOfWeek;
    //                return true;
    //            }
    //        }
    //        else
    //        {
    //            result = null;
    //            return true;
    //        }
    //    }

    //    private bool TryReadDayOfMonth(out int day)
    //    {
    //        string digits;
    //        if(TryGetNumDigits(2, out digits) || TryGetNumDigits(1, out digits))
    //        {
    //            CurrentIndex += digits.Length;
    //            day = int.Parse(digits);
    //            return true;
    //        }
    //        else
    //        {
    //            day = 0;
    //            return false;
    //        }
    //    }

    //    private bool TryReadMonth(out int month)
    //    {
    //        string chars;
    //        if(!TryGetCharacters(3, out chars))
    //        {
    //            month = 0;
    //            return false;
    //        }

    //        if(MonthsOfYear.TryGetValue(chars, out month))
    //        {
    //            CurrentIndex += chars.Length;
    //            return true;
    //        }
    //        else
    //        {
    //            return false;
    //        }
    //    }

    //    private bool TryReadDate(out int year, out int month, out int day)
    //    {
    //        if(State != LastReadState.Begin && State != LastReadState.CommaAfterDay)
    //        {
    //            throw new InvalidOperationException();
    //        }

    //        year = 0;
    //        month = 0;
    //        day = 0;

    //        if(!TryReadDayOfMonth(out day))
    //        {
    //            return false;
    //        }

    //        if(!AdvanceAtLeastOneSpace())
    //        {
    //            return false;
    //        }

    //        if(!TryReadMonth(out month))
    //        {
    //            return false;
    //        }

    //        if(!AdvanceAtLeastOneSpace())
    //        {
    //            return false;
    //        }


    //    }

    //    public static bool TryParse(string value, out DateTime result)
    //    {
    //        DateTime temp = new DateTime();

    //        result = new DateTime();

    //        var parser = new RFC822DateTimeParsingStateMachine(value);

    //        DayOfWeek? dayOfWeek;
    //        if(!parser.TryReadDay(out dayOfWeek))
    //        {
    //            return false;
    //        }

    //        if(!parser.AdvanceAtLeastOneSpace())
    //        {
    //            return false;
    //        }

    //        int year, month, day;
    //        if(!parser.TryReadDate(out year, out month, out day))
    //        {
    //            return false;
    //        }

    //        temp = new DateTime(year: year, month: month, day: day, hour: hour, minute: minute, second: second)

    //        if(dayOfWeek.HasValue)
    //        {
    //            if(temp.DayOfWeek != dayOfWeek)
    //            {
    //                temp.
    //            }
    //        }

    //        result = temp;
    //        return true;
    //    }
    //}

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
