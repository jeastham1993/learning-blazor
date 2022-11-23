namespace GetProducts;

using System.Text.Json.Serialization;

public record AppSyncPayload<TInputType>
{
    [JsonPropertyName("field")]
    public string Field { get; set; }
    
    [JsonPropertyName("arguments")]
    public AppSyncInput<TInputType> Arguments { get; set; }
}

public record AppSyncInput<TInputType>
{
    [JsonPropertyName("input")]
    public TInputType Input { get; set; }
}

public record CreateDataPointRequest
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("createdAt")]
    public string CreatedAt { get; set; }

    [JsonPropertyName("value")]
    public int Value { get; set; }
}