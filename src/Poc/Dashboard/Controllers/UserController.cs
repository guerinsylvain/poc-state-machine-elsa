using System;
using System.Threading;
using System.Threading.Tasks;
using Dashboard.Application.Models;
using Elsa.Activities.Workflows.Extensions;
using Elsa.Models;
using Elsa.Services;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Modes;

namespace Dashboard.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IWorkflowInvoker _workflowInvoker;

        public UserController(IWorkflowInvoker workflowInvoker)
        {
            _workflowInvoker = workflowInvoker;
        }

        [HttpGet]
        [Route("{Id}")]
        public IActionResult GetById(Guid id)
        {
            return Ok(new {Id = id});
        }


        [HttpPost]
        [Route("")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegistrationModel registration, CancellationToken cancellationToken)
        {
            // This is a test t check that we can trigger a workflow programmatically
            var input = new Variables();
            input.SetVariable("RegistrationModel", registration);

            await _workflowInvoker.TriggerSignalAsync("RegisterUser", input, null, null, cancellationToken);

            return Ok(); // Should be created...
        }
    }
}
