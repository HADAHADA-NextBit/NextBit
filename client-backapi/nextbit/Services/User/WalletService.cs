using MongoDB.Driver;
using nextbit.Databases.Enums;
using nextbit.Exceptions;

namespace nextbit.Services.User
{
    public class WalletService: UserBaseService
    {
        public WalletService(IServiceProvider serviceProvider): base(serviceProvider) 
        {
        }

        public Models.Wallet.WalletByCoin CreateWalletByCoin(
            Databases.Models.User user,
            CoinCode coinCode,
            decimal balance)
        {
            var walletValidation = MongoContext.Wallets.AsQueryable()
                .SingleOrDefault(x => x.UserId == user.Id && x.CoinCode == coinCode);

            if (walletValidation != null)
            {
                throw new BadRequestException("Wallet already exists.", -800);
            }

            var walletByCoin = new Models.Wallet.WalletByCoin(coinCode, balance);

            var wallet = new Databases.Models.Wallet()
            {
                UserId = user.Id,
                CoinCode = coinCode,
                Address = walletByCoin.Address,
                DestinationTag = (coinCode != CoinCode.XRP)
                    ? null
                    : walletByCoin.DestinationTag,
                OriginalBalance = balance,
                CreatedDate = DateTime.UtcNow,
            };

            MongoContext.Wallets.InsertOne(wallet);

            return walletByCoin;
        }

        public List<Models.Wallet.WalletByCoin> CreateWalletByCoinList(
            Databases.Models.User user, 
            decimal balance)
        {
            var walletByCoinList = new List<Models.Wallet.WalletByCoin>();

            foreach (CoinCode coinCode in Enum.GetValues(typeof(CoinCode)))
            {
                var walletByCoin = new Models.Wallet.WalletByCoin(coinCode, balance);

                var wallet = new Databases.Models.Wallet()
                {
                    UserId = user.Id,
                    CoinCode = coinCode,
                    Address = walletByCoin.Address,
                    DestinationTag = (coinCode != CoinCode.XRP)
                        ? null
                        : walletByCoin.DestinationTag,
                    OriginalBalance = balance,
                    CreatedDate = DateTime.UtcNow,
                };

                MongoContext.Wallets.InsertOne(wallet);

                walletByCoinList.Add(walletByCoin);
            }

            return walletByCoinList;
        }

        public Models.Wallet.WalletByCoin GetWalletByCoin(Databases.Models.User user, CoinCode coinCode)
        {
            var wallet = MongoContext.Wallets.AsQueryable()
                .SingleOrDefault(x => x.UserId == user.Id && x.CoinCode == coinCode);

            if (wallet == null)
            {
                throw new NotFoundException("Wallet does not exist", -300);
            }

            return new Models.Wallet.WalletByCoin(wallet);
        }

        public List<Models.Wallet.WalletByCoin> GetWalletByCoinList(Databases.Models.User user)
        {
            var walletByCoinList = new List<Models.Wallet.WalletByCoin>();

            var wallets = MongoContext.Wallets.AsQueryable()
                .Where(x => x.UserId == user.Id);
            
            foreach (var wallet in wallets)
            {
                walletByCoinList.Add(new Models.Wallet.WalletByCoin(wallet));
            }

            return walletByCoinList;
        }


    }
}
