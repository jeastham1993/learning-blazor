namespace LearningBlazor;

using System.Runtime.CompilerServices;

public static class AppSyncHelper
{
    private static Dictionary<string, string> _authHeaders;
    private static string host;
    private static string apiKey;

    public static Dictionary<string, string> AppSyncAuthHeader
    {
        get
        {
            if (string.IsNullOrEmpty(host) || string.IsNullOrEmpty(apiKey))
            {
                throw new Exception(
                    "Configuration not initialized, please call the AppSyncHelper.InitializeSettings() method");
            }
            
            if (_authHeaders == null)
            {
                _authHeaders = new Dictionary<string, string>()
                {
                    {"host", host },
                    {"x-api-key", apiKey}
                };
            }

            return _authHeaders;
        }
    }

    public static void InitializeSettings(IConfiguration configuration)
    {
        host = configuration["apiEndpoint"];
        apiKey = configuration["apiKey"];
    }
}