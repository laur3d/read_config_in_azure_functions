using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;

namespace CI.Demo.DynamicOrchestrator.ShortNameOrchestrator
{
    public class AddShortNameGreetingActivity
    {
        [FunctionName(nameof(AddShortNameGreetingActivity))]
        public async Task<string> Run([ActivityTrigger] (Guid correlationId, string providedName) input)
        {
            return await Task.FromResult($"Zup {input.providedName}!?");
        }
    }
}
