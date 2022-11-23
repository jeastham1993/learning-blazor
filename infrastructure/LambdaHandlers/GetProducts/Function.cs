using System.Text.Json;
using System.Threading.Tasks;
using Amazon.Lambda.Core;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace GetProducts
{
    public class Function
    {
        public async Task<object> FunctionHandler(AppSyncPayload<CreateDataPointRequest> inputObject, ILambdaContext context)
        {
            return new
            {
                name = inputObject.Arguments.Input.Name,
                createdAt = inputObject.Arguments.Input.CreatedAt,
                value = inputObject.Arguments.Input.Value
            };
        }
    }
}