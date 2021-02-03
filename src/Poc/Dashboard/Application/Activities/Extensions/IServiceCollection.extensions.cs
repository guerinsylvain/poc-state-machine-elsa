using Microsoft.Extensions.DependencyInjection;

namespace Dashboard.Application.Activities.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddActivities(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddActivity<ActivateUser>()
                             .AddActivity<CreateUser>()
                             .AddActivity<SayHelloWorld>()
                             .AddActivity<WaitingForApproval>();

            return serviceCollection;
        }
    }
}
