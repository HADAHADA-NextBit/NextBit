using nextbit.Databases.Enums;
using nextbit.Exceptions;

namespace nextbit.Utils
{
    public static class DestinationTagGenerator
    {
        public static string GenerateDestinationTag(this CoinCode coinCode)
        {
            if (coinCode == CoinCode.XRP)
            {
                // TODO:

                return "";
            }
             
            throw new InternalServerErrorException("Only XRP can have its own destination tag", -9999);
        }
    }
}
