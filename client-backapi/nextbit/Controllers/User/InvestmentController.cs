using Microsoft.AspNetCore.Mvc;
using nextbit.Services.User;

namespace nextbit.Controllers.User
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class InvestmentController : UserBaseController
    {
        public InvestmentController(IServiceProvider serviceProvider) : base(serviceProvider)
        { 
        }

        [HttpPost("")]
        public List<Models.Wallet.WalletByCoin> SetInvestmentBalance(
            [FromBody] Models.Investment.Request request,
            [FromServices] UserService userService,
            [FromServices] WalletService walletService)
        {
            var token = Request.Headers["Authorization"];
            var user = userService.GetUser(token);

            return walletService.CreateWalletByCoinList(user, request.Balance);
        }

        [HttpGet("summary")]
        public void GetInvestmentSummary()
        {
        }

        [HttpGet("transaction")]
        public void GetInvestmentTransactionList()
        {
        }

        [HttpGet("transaction/{coin}")]
        public void GetInvestmentTransactionListByCoin()
            //[FromRoute] )
        {
        }

        [HttpPost("{type}")]
        public void CreateInvestmentTransaction()
        //[FromRoute] )
        {
        }

        //-investment-
        //- [POST] investment
        //- [GET] investment/summary
        //- [GET] investment/transaction 
        //- [GET] investment/transaction/{coin}
        //- [POST] investment/{type}
    }
}
