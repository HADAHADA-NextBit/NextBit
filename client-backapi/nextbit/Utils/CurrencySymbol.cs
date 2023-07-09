using nextbit.Databases.Enums;

namespace nextbit.Utils
{
    public static class CurrencySymbol
    {
        public static string ToCurrencySymbol(this Currency value)
        {
            switch (value)
            {
                case Currency.WON:
                    return "₩";
                case Currency.USD:
                    return "$";
                default:
                    return "NO_SYMBOL";
            }
        }
    }
}
