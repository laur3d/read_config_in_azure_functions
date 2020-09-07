using System;
using System.Threading.Tasks;
using CI.Demo.DynamicOrchestrator.LongNameOrchestrator;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;

namespace CI.Demo.DynamicOrchestrator
{
    public class DetermineOrchestratorToRunActivity
    {

        [FunctionName(nameof(DetermineOrchestratorToRunActivity))]
        public async Task<string> Run([ActivityTrigger] IDurableActivityContext  context)
        {
            var input= context.GetInput<string>();
            string nameOfOrchestrator;

            switch (input.Length)
            {
               case <= 4 :
                   return nameof(ShortNameOrchestrator);
                   break;
               default:
                   return nameof(LongNameOrchestrator);
                   break;
            }
        }
    }
}
