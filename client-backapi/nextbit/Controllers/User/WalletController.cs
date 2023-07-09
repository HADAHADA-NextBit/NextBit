using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using nextbit.Databases.Enums;
using nextbit.Services.User;
using System.Security.Claims;

namespace nextbit.Controllers.User
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class WalletController: UserBaseController
    {
        public WalletController(IServiceProvider serviceProvider): base(serviceProvider)
        { 
        }

        [HttpGet("")]
        public List<Models.Wallet.WalletByCoin> GetWalletByCoinList(
            [FromServices] UserService userService,
            [FromServices] WalletService walletService)
        {
            var token = Request.Headers["Authorization"];
            var user = userService.GetUser(token);

            return walletService.GetWalletByCoinList(user);
        }

        [HttpGet("{coin}")]
        public Models.Wallet.WalletByCoin GetWalletByCoin(
            [FromRoute] CoinCode coin,
            [FromServices] UserService userService,
            [FromServices] WalletService walletService)
        {
            var token = Request.Headers["Authorization"];
            var user = userService.GetUser(token);

            return walletService.GetWalletByCoin(user, coin);
        }

        [HttpPost("")]
        public List<Models.Wallet.WalletByCoin> CreateWalletByCoinList(
            [FromBody] Models.Wallet.Request request,
            [FromServices] UserService userService,
            [FromServices] WalletService walletService)
        {
            var token = Request.Headers["Authorization"];
            var user= userService.GetUser(token);

            //======
            // TOOD: unauthorized exception -> check it
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var test = claimsIdentity;
            //======
            
            return walletService.CreateWalletByCoinList(user, request.Balance); 
        }

        [HttpPost("{coin}")]
        public Models.Wallet.WalletByCoin CreateWalletByCoin(
            [FromRoute] CoinCode coin,
            [FromBody] Models.Wallet.Request request,
            [FromServices] UserService userService,
            [FromServices] WalletService walletService)
        {
            var token = Request.Headers["Authorization"];
            var user = userService.GetUser(token);

            return walletService.CreateWalletByCoin(user, coin, request.Balance);
        }

        // ========================
        // transactoin

        /// <summary>
        /// bid / ask 거래 생성
        /// </summary>
        [HttpPost("transaction")]
        public Models.Transaction.History CreateTransaction(
            [FromBody] Models.Transaction.Request request,
            [FromServices] UserService userService,
            [FromServices] TransactionService transactionServicev)
        {
            var token = Request.Headers["Authorization"];
            var user = userService.GetUser(token);

            return transactionServicev.CreateTransaction(
                user, 
                request.TransactionType, 
                request.CoinCode, 
                request.Amount,
                request.Price);
        }

        /// <summary>
        /// 매수/매도 거래 내역 
        /// </summary>
        [HttpGet("transaction/{type?}")]
        public List<Models.Transaction.History> GetTransactionHistories(
            [FromRoute] TransactionType? type,
            [FromServices] UserService userService,
            [FromServices] TransactionService transactionServicev)
        {
            var token = Request.Headers["Authorization"];
            var user = userService.GetUser(token);

            return transactionServicev.GetTransactionHistories(
                user, type);
        }


    }
}
