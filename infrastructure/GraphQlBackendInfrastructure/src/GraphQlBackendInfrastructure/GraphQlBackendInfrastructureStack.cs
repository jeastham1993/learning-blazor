using Amazon.CDK;
using Constructs;

namespace GraphQlBackendInfrastructure
{
    public class GraphQlBackendInfrastructureStack : Stack
    {
        internal GraphQlBackendInfrastructureStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            var persistenceStack = new PersistenceStack(
                this,
                "PersistenceLayer");

            var graphQl = new BackendApiStack(
                this,
                "BackendApi",
                new BackendApiStackProps(persistenceStack.DataPointTable));
        }
    }
}
