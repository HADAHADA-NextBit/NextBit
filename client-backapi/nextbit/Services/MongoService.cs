using nextbit.Databases;
using nextbit.Services;

namespace nextbit.Services
{
    public abstract class MongoService : BaseService
    {
        public MongoContext MongoContext { get; }

        public MongoService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            MongoContext = ServiceProvider.GetRequiredService<MongoContext>();
        }
    }
}
