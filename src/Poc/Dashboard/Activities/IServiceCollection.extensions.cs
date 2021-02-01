using Microsoft.Extensions.DependencyInjection;

namespace Dashboard.Activities
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddActivities(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddActivity<CreateUser>()
                .AddActivity<SayHelloWorld>();

            return serviceCollection;
        }
    }
}
