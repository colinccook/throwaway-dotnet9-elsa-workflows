using Elsa.Extensions;
using Elsa.Workflows;

public class RetrieveCurrentBroadbandPackageConfiguration : Activity
{
    protected override ValueTask ExecuteAsync(ActivityExecutionContext context)
    {
        Thread.Sleep(10000); // Simulate a delay of 2 seconds
        context.SetVariable("hasMatchingBroadbandPackageConfiguration", "true"); // Set the variable with the value
        return ValueTask.CompletedTask;
    }
}