using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;

namespace CI.Demo.DynamicOrchestrator.LongNameOrchestrator
{
    public class AddLongNameGreetingActivity
    {
        [FunctionName(nameof(AddLongNameGreetingActivity))]
        public async Task<string> Run([ActivityTrigger] IDurableActivityContext  context)
        {
            (Guid correlationId, string providedName) input= context.GetInput<(Guid, string)>();
            return await Task.FromResult($"Greeting dear {input.providedName}, how wonderful to meet you!");
        }
    }
}
