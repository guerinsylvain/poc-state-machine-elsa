using System;
using System.IO;
using Elsa.Attributes;
using Elsa.Models;
using Elsa.Results;
using Elsa.Services;
using Elsa.Services.Models;
using NodaTime;

namespace Dashboard.Application.Activities
{
    [ActivityDefinition(Category = "iBadge", Description = "Create a User")]
    public class CreateUser : Activity
    {
        private readonly TextWriter _writer;

        public CreateUser(TextWriter writer)
        {
            _writer = writer;
        }

        protected override ActivityExecutionResult OnExecute(WorkflowExecutionContext context)
        {
            var log = new LogEntry(context.CurrentActivity.Id, Instant.FromDateTimeUtc(DateTime.UtcNow), "User created");
            context.Workflow.ExecutionLog.Add(log);

            return Done();
        }
    }
}
