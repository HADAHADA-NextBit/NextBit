using System.Reflection;

namespace nextbit.Services
{
    public abstract class BaseService
    {
        public IServiceProvider ServiceProvider { get; }

        public BaseService(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
    }


    public static class BaseServiceExtension
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var targetType = typeof(BaseService);
            var types = assembly.GetTypes().Where(a =>
                a.IsSubclassOf(targetType)
                    && a.IsAbstract == false
                    && a.DeclaringType != targetType);

            foreach (var item in types)
            {
                services.AddScoped(item);
            }

            return services;
        }
    }

}
