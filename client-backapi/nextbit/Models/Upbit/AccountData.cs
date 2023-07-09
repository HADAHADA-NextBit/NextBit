using nextbit.Databases.Enums;

namespace nextbit.Models.Upbit
{
    public class AccountData
    {
        public string currency { get; set; }
        public string balance { get; set; }
        public string locked { get; set; }
        public string avg_buy_price { get; set; }
        public bool avg_buy_price_modified { get; set; }
        public string unit_currency { get; set; }
    }
}
