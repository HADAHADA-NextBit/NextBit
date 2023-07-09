using nextbit.Databases.Enums;

namespace nextbit.Utils
{
    public static class WalletAddressGenerator
    {
        public static string GenerateAddress(this CoinCode coinCode)
        {
            // TODO: 
            switch (coinCode)
            {
                case CoinCode.BTC:
                    break;
                case CoinCode.ETH:
                    break;
                case CoinCode.XRP:
                    break;
                case CoinCode.USDT:
                    break;
                default:
                    break;
            }

            return "";
        }
    }
}
