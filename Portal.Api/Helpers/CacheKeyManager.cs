namespace Portal.Api.Helpers;

public static class CacheKeyManager
{
    public static string GetCasheKey(string key) => $"cache_{key}";
    public static string GetCasheKey(string key, string args) => $"cache_{key}_{args}";
}

