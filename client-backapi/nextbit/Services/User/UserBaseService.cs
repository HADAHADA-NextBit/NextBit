namespace nextbit.Services.User
{
    public class UserBaseService: MongoService
    {
        public IHttpContextAccessor HttpContextAccessor { get; }

        public UserBaseService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            HttpContextAccessor = ServiceProvider.GetRequiredService<IHttpContextAccessor>();
        }
    }
}
