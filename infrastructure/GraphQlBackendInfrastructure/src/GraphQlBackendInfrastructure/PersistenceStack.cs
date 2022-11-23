using Constructs;

namespace GraphQlBackendInfrastructure
{
    using Amazon.CDK.AWS.DynamoDB;
    using Amazon.JSII.Runtime.Deputy;

    public class PersistenceStack : Construct
    {
        public Table DataPointTable { get; private set; }
        /// <inheritdoc />
        public PersistenceStack(Construct scope, string id) : base(scope,
            id)
        {
            DataPointTable = new Table(
                scope,
                "DataPointTable",
                new TableProps()
                {
                    PartitionKey = new Attribute()
                    {
                        Name = "name",
                        Type = AttributeType.STRING
                    },
                    SortKey = new Attribute()
                    {
                        Name = "createdAt",
                        Type = AttributeType.STRING
                    }
                });
        }
    }
}