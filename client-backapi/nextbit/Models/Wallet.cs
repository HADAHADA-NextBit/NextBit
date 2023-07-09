using nextbit.Databases.Enums;
using nextbit.Utils;

namespace nextbit.Models
{
    public class Wallet
    {
        public class Request
        {
            //public List<CoinCode> coinCodes = new List<CoinCode>();
            public decimal Balance { get; set; } 
        }

        public class WalletByCoin
        {
            public CoinCode CoinCode { get; set; }
            public string Address { get; set; } = string.Empty;
            public string? DestinationTag { get; set; }

            /// <summary>
            /// 최초 지갑 생성시 투자 유치금 
            /// </summary>
            public decimal OriginalBalance { get; set; }

            /// <summary>
            /// 코인별 매수/매도에서 +/- 된 금액 
            /// </summary>
            public decimal Balance { get; set; } = 0.0m;

            /// <summary>
            /// 코일별 매수/매도에서 +/-된 코인 수량 
            /// </summary>
            public decimal Amount { get; set; } = 0.0m;


            public WalletByCoin()
            { 
            }

            public WalletByCoin(CoinCode coinCode, decimal balance)
            {
                CoinCode = coinCode;
                Address = coinCode.GenerateAddress();

                if (coinCode == CoinCode.XRP)
                {
                    DestinationTag = coinCode.GenerateDestinationTag();
                }

                OriginalBalance = balance;
            }

            public WalletByCoin(Databases.Models.Wallet wallet)
            { 
                CoinCode = wallet.CoinCode;
                Address = wallet.Address;
                DestinationTag = wallet.DestinationTag; 
                OriginalBalance = wallet.OriginalBalance;
                Balance = wallet.Balance;
                Amount = wallet.Amount;
            }
        }
        
    }
}
