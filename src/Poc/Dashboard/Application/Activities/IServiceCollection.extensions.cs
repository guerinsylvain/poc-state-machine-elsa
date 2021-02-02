﻿using Microsoft.Extensions.DependencyInjection;

namespace Dashboard.Application.Activities
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddActivities(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddActivity<CreateUser>()
                             .AddActivity<SayHelloWorld>()
                             .AddActivity<WaitingForApproval>();

            return serviceCollection;
        }
    }
}
