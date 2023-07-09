using nextbit.Databases.Enums;

namespace nextbit.Models
{
    public class Transaction
    {
        public class History
        {
            public CoinCode CoinCode { get; set; }
            public TransactionType TransactionType { get; set; }
            /// <summary>
            /// 거래 코인 수량 
            /// </summary>
            public decimal Amount { get; set; }
            /// <summary>
            /// 거래 코인 수량별 금액
            /// </summary>
            public decimal Balance { get; set; }
            /// <summary>
            /// 수익률
            /// </summary>
            public decimal ProfitRate { get; set; }
            /// <summary>
            /// 수익 금액
            /// </summary>
            public decimal ProfitAmount { get; set; }
            /// <summary>
            /// 수수료 (0.1%에 해당하는 금액)
            /// </summary>
            public decimal Fee { get; set; }
            /// <summary>
            /// 이전 금액
            /// </summary>
            public decimal ProfitBefore { get; set; }
            /// <summary>
            /// 이후 금액
            /// </summary>
            public decimal ProfitAfter { get; set; }

            public History()
            { 
            }

            public History(Databases.Models.Transaction transaction)
            {
                // TODO: 확인 
                CoinCode = transaction.CoinCode;
                TransactionType = transaction.TransactionType;
                //Amount = transaction.Amount;
                //Balance = ;
                //ProfitRate = ;
                //ProfitAmount = ;
                //Fee = Balance * 0.01m;
                //ProfitBefore = ;
                //ProfitAfter = ;
            }
        }

        public class Request
        {
            public CoinCode CoinCode { get; set; }
            public TransactionType TransactionType { get; set;}
            public decimal Amount { get; set; } 
            public decimal Price { get; set; }
        }
    }
}
