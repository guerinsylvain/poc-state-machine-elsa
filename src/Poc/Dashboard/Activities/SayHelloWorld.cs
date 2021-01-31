using System;
using System.IO;
using Elsa.Attributes;
using Elsa.Models;
using Elsa.Results;
using Elsa.Services;
using Elsa.Services.Models;
using NodaTime;

namespace Dashboard.Activities
{
    [ActivityDefinition(Category = "iBadge", Description = "Say hello to the world")]
    public class SayHelloWorld : Activity
    {
        private readonly TextWriter _writer;

        public SayHelloWorld(TextWriter writer)
        {
            _writer = writer;
        }

        protected override ActivityExecutionResult OnExecute(WorkflowExecutionContext context)
        {
            var log = new LogEntry(context.CurrentActivity.Id, Instant.FromDateTimeUtc(DateTime.UtcNow), "Hello World!");
            context.Workflow.ExecutionLog.Add(log);

            return Done();
        }
    }
}
