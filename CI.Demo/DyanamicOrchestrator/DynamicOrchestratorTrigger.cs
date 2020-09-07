using System;
using System.IO;
using System.Threading.Tasks;
using CI.Demo.DynamicOrchestrator.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CI.Demo.DynamicOrchestrator
{
    public static class DynamicOrchestratorTrigger
    {
        [FunctionName("DynamicOrchestratorTrigger")]
        public static async Task<IActionResult> RunAsync(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]
            HttpRequest req, ILogger log,
            ExecutionContext executionContext,
            [DurableClient] IDurableOrchestrationClient starter)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name ??= data?.name;
            if (name == null)
            {
                return new BadRequestObjectResult("Please pass a name on the query string or in the request body");
            }

            var orchestratorInput = new OrchestratorInput
            {
                CorrelationId =  new Guid(),
                Name =  name.ToString()
            };

            var instanceId = await starter.StartNewAsync(nameof(DynamicOrchestrator), orchestratorInput);

            DurableOrchestrationStatus durableOrchestrationStatus = await starter.GetStatusAsync(instanceId);
            while (durableOrchestrationStatus.RuntimeStatus != OrchestrationRuntimeStatus.Completed)
            {
                await Task.Delay(200);
                durableOrchestrationStatus = await starter.GetStatusAsync(instanceId);
            }

            return (ActionResult) new OkObjectResult(durableOrchestrationStatus.Output);
        }
    }
}
