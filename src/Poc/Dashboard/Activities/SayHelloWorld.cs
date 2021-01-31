using System;
using Elsa.Attributes;
using Elsa.Results;
using Elsa.Services;
using Elsa.Services.Models;

namespace Dashboard.Activities
{
    [ActivityDefinition(Category = "iBadge", Description = "Say hello to the world")]
    public class SayHelloWorld : Activity
    {
        protected override ActivityExecutionResult OnExecute(WorkflowExecutionContext context)
        {
            Console.WriteLine("Hello World!");

            return Done();
        }
    }
}
