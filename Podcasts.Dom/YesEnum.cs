namespace Podcasts.Dom
{
    public enum YesEnum
    {
        Yes,
        No,
    }

    internal static class YesEnumHelper
    {
        public static YesEnum? TryParseYesEnum(string value)
        {
            if (value == "yes") return YesEnum.Yes;
            if (value == "no") return YesEnum.No;
            return null;
        }
    }
}