using System;
using System.Threading;
using System.Threading.Tasks;
using Dashboard.Application.Models;
using Elsa.Attributes;
using Elsa.Expressions;
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
        [ActivityProperty(Hint = "Enter an expression that evaluates to the name of the user to create.")]
        public WorkflowExpression<string> UserName
        {
            get => GetState<WorkflowExpression<string>>();
            set => SetState(value);
        }

        [ActivityProperty(Hint = "Enter an expression that evaluates to the email address of the user to create.")]
        public WorkflowExpression<string> Email
        {
            get => GetState<WorkflowExpression<string>>();
            set => SetState(value);
        }

        [ActivityProperty(Hint = "Enter an expression that evaluates to the password of the user to create.")]
        public WorkflowExpression<string> Password
        {
            get => GetState<WorkflowExpression<string>>();
            set => SetState(value);
        }

        protected override async Task<ActivityExecutionResult> OnExecuteAsync(WorkflowExecutionContext context, CancellationToken cancellationToken)
        {
            var name = await context.EvaluateAsync(UserName, cancellationToken);
            var email = await context.EvaluateAsync(Email, cancellationToken);
            var password = await context.EvaluateAsync(Password, cancellationToken);

            // should create the user in the DB here....
            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Name = name,
                Email = email,
                Password = password, // password should be hashed...
                IsActive = false
            };

            var log = new LogEntry(context.CurrentActivity.Id, Instant.FromDateTimeUtc(DateTime.UtcNow), $"User '{name}'/'{email}' with password '{password}' created");
            context.Workflow.ExecutionLog.Add(log);

            Output.SetVariable("User", user);

            return Done();
        }
    }
}
