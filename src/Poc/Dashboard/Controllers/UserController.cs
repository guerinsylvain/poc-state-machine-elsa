using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dashboard.Application.Models;
using Elsa.Models;
using Elsa.Persistence;
using Elsa.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IWorkflowInvoker _workflowInvoker;
        private readonly IWorkflowInstanceStore _workflowInstanceStore;
        private readonly IWorkflowRegistry _workflowRegistry;

        public UserController(IWorkflowInvoker workflowInvoker, IWorkflowRegistry workflowRegistry, IWorkflowInstanceStore workflowInstanceStore)
        {
            _workflowInvoker = workflowInvoker;
            _workflowInstanceStore = workflowInstanceStore;
            _workflowRegistry = workflowRegistry;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegistrationModel registration, CancellationToken cancellationToken)
        {
            // THIS IS A TEST TO CHECK THAT WE CAN TRIGGER A WORKFLOW PROGRAMMATICALLY

            // Get the version '20' of the worklow 'Register User' 
            // '08ee70d9fef040a0996e58e16d12deab' is the Definition Id found in the table 'WorkflowDefinitionVersions' 
            var workflowDefinition = await _workflowRegistry.GetWorkflowDefinitionAsync("08ee70d9fef040a0996e58e16d12deab",
                                                                                        VersionOptions.SpecificVersion(20),
                                                                                        cancellationToken);

            var input = new Variables();
            input.SetVariable("RegistrationModel", registration);

            var context = await _workflowInvoker.StartAsync(workflowDefinition, input, startActivityIds: null, correlationId: null, cancellationToken);

            if (context.Workflow.Fault != null)
            {
                return BadRequest(context.Workflow.Fault.Message);
            }

            return Ok(); 
        }

        [HttpPost]
        [Route("{id}/approve-registration")]
        public async Task<IActionResult> ApproveRegistrationAsync(Guid id, CancellationToken cancellationToken)
        {
            // We need to find if there is a "Register User" workflow instance blocked on the 'WaitingForApproval' activity
            // for the user 'id'.
            var workflows = await _workflowInstanceStore.ListByBlockingActivityAsync("WaitingForApproval");

            workflows =  workflows.Where(w => w.Item1.DefinitionId == "08ee70d9fef040a0996e58e16d12deab" &&
                                              w.Item1.Scope.GetVariable<string>("UserId").Equals(id.ToString(), StringComparison.OrdinalIgnoreCase));

            // TODO: check that one and only one workflow instance is found...
            //       and return BAD REQUEST + error info if needed
            
            var workflowInstance = workflows.First().Item1;
            await _workflowInvoker.ResumeAsync(workflowInstance, 
                                               input: new Variables() { ["Test"] = new Variable("XXXX") }, 
                                               startActivityIds: workflowInstance.BlockingActivities.Select(s => s.ActivityId), 
                                               cancellationToken: cancellationToken);

            return Ok();
        }
    }
}
