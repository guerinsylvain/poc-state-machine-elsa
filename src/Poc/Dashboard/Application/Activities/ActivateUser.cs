using System.Threading;
using System.Threading.Tasks;
using Elsa;
using Elsa.Attributes;
using Elsa.Expressions;
using Elsa.Results;
using Elsa.Services;
using Elsa.Services.Models;

namespace Dashboard.Application.Activities
{
    [ActivityDefinition(Category = "iBadge", Description = "Activate a User", Icon = "fas fa-user-check", Outcomes = new[] { OutcomeNames.Done })]
    public class ActivateUser: Activity
    {
        [ActivityProperty(Hint = "Enter an expression that evaluates to the ID of the user to activate.")]
        public WorkflowExpression<string> UserId
        {
            get => GetState<WorkflowExpression<string>>();
            set => SetState(value);
        }

        protected override async Task<ActivityExecutionResult> OnExecuteAsync(WorkflowExecutionContext context, CancellationToken cancellationToken)
        {
            var userId = await context.EvaluateAsync(UserId, cancellationToken);

            // TODO: Get user from db and set its Active property to true

            return Done();
        }
    }
}
