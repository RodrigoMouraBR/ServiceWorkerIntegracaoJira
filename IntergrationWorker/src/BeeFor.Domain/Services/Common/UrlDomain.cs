namespace BeeFor.Domain.Services.Common
{
    public static class UrlDomain
    {
        public static string MontarApiBaseUrl(string baseUrlJira)
        {
            var apiBaseUrl = baseUrlJira;
            if (!apiBaseUrl.StartsWith("http"))
                apiBaseUrl = $"https://{apiBaseUrl}";

            if (!apiBaseUrl.EndsWith("/"))
                apiBaseUrl += "/";
            return apiBaseUrl;
        }
    }
}
