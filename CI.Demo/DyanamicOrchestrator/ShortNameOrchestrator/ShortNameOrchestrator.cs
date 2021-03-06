﻿using System;
using System.Threading.Tasks;
using CI.Demo.DynamicOrchestrator.Common;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;

namespace CI.Demo.DynamicOrchestrator.ShortNameOrchestrator
{
    public class ShortNameOrchestrator
    {
        [FunctionName(nameof(ShortNameOrchestrator))]
        public async Task<string> Run([OrchestrationTrigger] IDurableOrchestrationContext context,
                                              ExecutionContext executionContext)
        {
            var input = context.GetInput<OrchestratorInput>();

            return await context.CallActivityAsync<string>(nameof(AddShortNameGreetingActivity), (input.CorrelationId, input.Name));;
        }
    }
}
