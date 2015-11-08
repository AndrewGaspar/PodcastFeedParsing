using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Podcasts.Dom
{
    public enum ExplicitEnum
    {
        Yes,
        Clean,
    }

    internal static class ExplicitEnumHelper
    {
        public static ExplicitEnum? TryParseExplicitEnum(string text)
        {
            if (text == "yes") return ExplicitEnum.Yes;
            if (text == "clean") return ExplicitEnum.Clean;
            return null;
        }
    }
}
