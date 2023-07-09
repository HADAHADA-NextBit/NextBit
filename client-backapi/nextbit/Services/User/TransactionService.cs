using MongoDB.Driver;
using nextbit.Databases.Enums;
using RestSharp;
using System.Net;
using System.Net.WebSockets;
using System.Security.AccessControl;

namespace nextbit.Services.User
{
    public class TransactionService : UserBaseService
    {
        public TransactionService(IServiceProvider serviceProvider): base(serviceProvider) 
        {
        }
        
        public List<Models.Transaction.History> GetTransactionHistories(
            Databases.Models.User user,
            TransactionType? transactionType = null)
        {
            var transactions = MongoContext.Transactions.AsQueryable()
                .Where(x => x.UserId == user.Id)
                .OrderByDescending(x => x.CreatedDate)
                .ToList()
                .AsQueryable();

            if (transactionType != null)
            {
                transactions = transactions
                    .Where(x => x.TransactionType == transactionType);
            }

            var transactionHistories = new List<Models.Transaction.History>();
            
            foreach (var transaction in transactions) 
            {
                transactionHistories.Add(new Models.Transaction.History(transaction));
            }

            return transactionHistories;
        }
        
        public Models.Transaction.History CreateTransaction(
            Databases.Models.User user, 
            TransactionType transactionType,
            CoinCode coinCode,
            decimal amount,
            decimal price)
        {
            var transaction = new Databases.Models.Transaction()
            { 
                UserId = user.Id,   
                TransactionType = transactionType,
                CoinCode = coinCode,
                Amount = amount,
                Price = price, 
                CreatedDate = DateTime.UtcNow
            };

            MongoContext.Transactions.InsertOne(transaction);

            // TODO: wallet 에서 +/- 
            //var wallet = MongoContext.Wallets.AsQueryable()
            //    .SingleOrDefault(x => x.UserId == user.Id && x.CoinCode == coinCode);
            var balanceBefore = (MongoContext.Wallets.AsQueryable()
                .SingleOrDefault(x => x.UserId == user.Id && x.CoinCode == coinCode)
                ?.Balance ?? 0);
            var amountBefore = (MongoContext.Wallets.AsQueryable()
                            .SingleOrDefault(x => x.UserId == user.Id && x.CoinCode == coinCode)
                            ?.Amount ?? 0);

            switch (transactionType)
            {
                case TransactionType.Bid:
                    {
                        MongoContext.Wallets.FindOneAndUpdate(
                            Builders<Databases.Models.Wallet>.Filter.Eq(x => x.UserId, user.Id),
                            Builders<Databases.Models.Wallet>.Update.Set(x => x.Balance, (balanceBefore + price))
                                                                    .Set(x => x.Amount, (amountBefore + amount)));
                    }
                    break;
                case TransactionType.Ask:
                    {
                        MongoContext.Wallets.FindOneAndUpdate(
                            Builders<Databases.Models.Wallet>.Filter.Eq(x => x.UserId, user.Id),
                            Builders<Databases.Models.Wallet>.Update.Set(x => x.Balance, (balanceBefore - price))   
                                                                    .Set(x => x.Amount, (amountBefore - amount)));
                    }
                    break;
                default:
                    break;
            }

            // TODO: update summary as well ?? 

            return new Models.Transaction.History(transaction);
        }


    }
}
