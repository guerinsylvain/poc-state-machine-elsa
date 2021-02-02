using Elsa.Attributes;
using Elsa.Results;
using Elsa.Services;
using Elsa.Services.Models;

namespace Dashboard.Application.Activities
{
    [ActivityDefinition(Category = "iBadge", Description = "Waiting for Approval")]
    public class WaitingForApproval : Activity
    {
        protected override ActivityExecutionResult OnExecute(WorkflowExecutionContext context)
        {
            return Halt();
        }

        protected override ActivityExecutionResult OnResume(WorkflowExecutionContext context)
        {
            return Done();
        }
    }
}
