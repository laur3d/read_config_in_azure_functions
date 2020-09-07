using System.Threading.Tasks;
using CI.Demo.DynamicOrchestrator.Common;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;

namespace CI.Demo.DynamicOrchestrator
{
    public class DynamicOrchestrator
    {

        [FunctionName(nameof(DynamicOrchestrator))]
        public async Task<string> Run([OrchestrationTrigger] IDurableOrchestrationContext context,
            ExecutionContext executionContext)
        {
            var input = context.GetInput<OrchestratorInput>();
            return await context.CallSubOrchestratorAsync<string>(
                await context.CallActivityAsync<string>(
                    nameof(DetermineOrchestratorToRunActivity), input.Name)
                ,input);
        }
    }
}
