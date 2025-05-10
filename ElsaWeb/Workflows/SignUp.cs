using Elsa.Http;
using Elsa.Workflows;
using Elsa.Workflows.Activities;
using Elsa.Workflows.Activities.Flowchart.Activities;
using Endpoint = Elsa.Workflows.Activities.Flowchart.Models.Endpoint;

namespace ElsaWeb.Workflows;

public class ValidateOpeningRead : WorkflowBase
{
    protected override void Build(IWorkflowBuilder builder)
    {
        builder.Name = "Sign up";
        builder.Description = "Onboarding a broadband customer";
        builder.Version = 1;

        var hasMatchingBroadbandPackageConfiguration = builder.WithVariable<bool>();

        var initialActivity = new HttpEndpoint { Path = new("/sign-up"), CanStartWorkflow = true };
        var retrieveCurrentBroadbandPackageConfiguration = new RetrieveCurrentBroadbandPackageConfiguration();
        var matchingBroadbandPackageConfiguration = new FlowDecision(context => hasMatchingBroadbandPackageConfiguration.Get(context) == true);
        var reinsertReadingAgainstNewRegisterId = new WriteHttpResponse { Content = new("Customer already has correct broadband package configuration") };
        var requestReconfiguration = new WriteLine("Request reconfiguration from OpenReach");
        var requestedReconfiguration = new WriteLine("Requested reconfiguration from OpenReach");
        var requestedReconfigurationOverdue = new WriteLine("Requested reconfiguration from OpenReach overdue");
        var customerNowHasCorrectBroadbandPackageConfiguration = new WriteHttpResponse { Content = new("Customer now has correct broadband package configuration") };

        builder.Root = new Flowchart
        {
            Activities =
            {
                initialActivity,
                retrieveCurrentBroadbandPackageConfiguration,
                matchingBroadbandPackageConfiguration,
                reinsertReadingAgainstNewRegisterId,
                requestReconfiguration,
                requestedReconfiguration,
                requestedReconfigurationOverdue,
                customerNowHasCorrectBroadbandPackageConfiguration
            },
            Connections =
            {
                new(new Endpoint(initialActivity), new Endpoint(retrieveCurrentBroadbandPackageConfiguration)),
                new(new Endpoint(retrieveCurrentBroadbandPackageConfiguration), new Endpoint(matchingBroadbandPackageConfiguration)),
                new(new Endpoint(matchingBroadbandPackageConfiguration, "True"), new Endpoint(reinsertReadingAgainstNewRegisterId)),
                new(new Endpoint(matchingBroadbandPackageConfiguration, "False"), new Endpoint(requestReconfiguration)),
                new(new Endpoint(requestReconfiguration), new Endpoint(requestedReconfiguration)),
                new(new Endpoint(requestedReconfiguration), new Endpoint(requestedReconfigurationOverdue)),
                new(new Endpoint(requestedReconfigurationOverdue), new Endpoint(customerNowHasCorrectBroadbandPackageConfiguration))
            }
        };
    }
}