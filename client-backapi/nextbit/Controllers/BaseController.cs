using Microsoft.AspNetCore.Mvc;
using MongoDbGenericRepository;
using nextbit.Databases;

namespace nextbit.Controllers
{
    public class BaseController: ControllerBase
    {      
        public IServiceProvider ServiceProvider { get; }
        public MongoContext MongoContext { get; }
        //public CredentialService CredentialService { get; }
        //public string? UserIdentifier { get => CredentialService.UserIdentifier; }
        //public int? UserSn { get => CredentialService.UserSn; }

        public BaseController(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            MongoContext = ServiceProvider.GetRequiredService<MongoContext>();
            //CredentialService = ServiceProvider.GetRequiredService<CredentialService>();
        }
    }
}
