using System;
using System.Threading;
using System.Threading.Tasks;
using Dashboard.Application.Models;
using Elsa.Models;
using Elsa.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IWorkflowInvoker _workflowInvoker;
        private readonly IWorkflowRegistry _workflowRegistry;

        public UserController(IWorkflowInvoker workflowInvoker, IWorkflowRegistry workflowRegistry)
        {
            _workflowInvoker = workflowInvoker;
            _workflowRegistry = workflowRegistry;
        }

        [HttpGet]
        [Route("{Id}")]
        public IActionResult GetById(Guid id)
        {
            return Ok(new { Id = id });
        }
        
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegistrationModel registration, CancellationToken cancellationToken)
        {
            // Get the version '7' of the worklow 'Register User' 
            // 08ee70d9fef040a0996e58e16d12deab is the Definition Id found in the table 'WorkflowDefinitionVersions'
            var workflowDefinition = await _workflowRegistry.GetWorkflowDefinitionAsync("08ee70d9fef040a0996e58e16d12deab", VersionOptions.SpecificVersion(7), cancellationToken);

            // This is a test t check that we can trigger a workflow programmatically
            var input = new Variables();
            input.SetVariable("RegistrationModel", registration);

            await _workflowInvoker.StartAsync(workflowDefinition, input, startActivityIds: null, correlationId: null, cancellationToken);

            return Ok(); // Should be http status 201 to be rest compliant...
        }
    }
}
