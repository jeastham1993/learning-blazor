
using Amazon.CDK.AWS.AppSync.Alpha;

using Constructs;

namespace GraphQlBackendInfrastructure;

using System.Collections.Generic;

using Amazon.CDK;
using Amazon.CDK.AWS.DynamoDB;
using Amazon.CDK.AWS.Lambda;
using Amazon.CDK.AWS.SES.Actions;

public record BackendApiStackProps(Table DataPointTable);

public class BackendApiStack : Construct
{
    /// <inheritdoc />
    public BackendApiStack(Construct scope, string id, BackendApiStackProps props) : base(scope,
        id)
    {
        var testLambdaFunction = Function.FromFunctionAttributes(
            scope,
            "TestLambdaFunction",
            new FunctionAttributes()
            {
                FunctionArn = "",
                SameEnvironment = true
            });
        
        var api = new GraphqlApi(
            scope,
            "GraphQlApi",
            new GraphqlApiProps()
            {
                Name = "GraphQlBackendApi",
                Schema = Schema.FromAsset("src/GraphQlBackendInfrastructure/appsync/schema.graphql"),
                AuthorizationConfig = new AuthorizationConfig()
                {
                    DefaultAuthorization = new AuthorizationMode()
                    {
                        AuthorizationType = AuthorizationType.API_KEY,
                        ApiKeyConfig = new ApiKeyConfig()
                        {
                            Name = "default",
                            Description = "Default auth mode",
                        }
                    }
                }
            });

        var lambdaDataSource = api.AddLambdaDataSource(
            "TestLambdaDataSource",
            testLambdaFunction);

        lambdaDataSource.CreateResolver(
            new ResolverProps()
            {
                TypeName = "Mutation",
                FieldName = "testAddDataPoint",
                RequestMappingTemplate = MappingTemplate.FromFile(
                    "src/GraphQlBackendInfrastructure/appsync/resolvers/Mutation.testAddDataPoint.req.vtl"),
                ResponseMappingTemplate = MappingTemplate.LambdaResult() 
            });
        
        var dataSource = api.AddDynamoDbDataSource(
            "DataPointTableDataSource",
            props.DataPointTable);

        dataSource.CreateResolver(
            new ResolverProps()
            {
                TypeName = "Mutation",
                FieldName = "createDataPoint",
                RequestMappingTemplate = MappingTemplate.FromFile("src/GraphQlBackendInfrastructure/appsync/resolvers/Mutation.createDataPoint.req.vtl"),
                ResponseMappingTemplate = MappingTemplate.DynamoDbResultItem()
            });

        dataSource.CreateResolver(
            new ResolverProps()
            {
                TypeName = "Query",
                FieldName = "listDataPoints",
                RequestMappingTemplate = MappingTemplate.FromFile("src/GraphQlBackendInfrastructure/appsync/resolvers/Query.queryListDataPoints.req.vtl"),
                ResponseMappingTemplate = MappingTemplate.DynamoDbResultItem()
            });

        dataSource.CreateResolver(
            new ResolverProps()
            {
                TypeName = "Query",
                FieldName = "getDataPoint",
                RequestMappingTemplate = MappingTemplate.FromFile("src/GraphQlBackendInfrastructure/appsync/resolvers/Query.queryGetDataPoint.req.vtl"),
                ResponseMappingTemplate = MappingTemplate.DynamoDbResultItem()
            });

        dataSource.CreateResolver(
            new ResolverProps()
            {
                TypeName = "Query",
                FieldName = "queryDataPointsByNameAndDateTime",
                RequestMappingTemplate = MappingTemplate.FromFile("src/GraphQlBackendInfrastructure/appsync/resolvers/Query.queryDataPointsByNameAndDateTime.req.vtl"),
                ResponseMappingTemplate = MappingTemplate.DynamoDbResultItem()
            });

        new CfnOutput(
            this,
            "GraphQL_ApiId",
            new CfnOutputProps()
            {
                Value = api.ApiId
            });

        new CfnOutput(
            this,
            "GraphQL_ApiUrl",
            new CfnOutputProps()
            {
                Value = api.GraphqlUrl
            });

        new CfnOutput(
            this,
            "GraphQL_ApiKey",
            new CfnOutputProps()
            {
                Value = api.ApiKey
            });
    }
}