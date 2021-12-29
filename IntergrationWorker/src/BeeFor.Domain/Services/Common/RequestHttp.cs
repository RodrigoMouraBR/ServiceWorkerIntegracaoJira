using BeeFor.Core.Utils;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BeeFor.Domain.Services.Common
{
    public static class RequestHttp
    {
        public static async Task<Stream> HTTP_GET(string usuario, string chave, string targetUrl)
        {
            HttpClient client = new HttpClient();

            var byteArray = Base64.Encode($"{usuario.Trim()}:{chave.Trim()}", false);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", byteArray);

            HttpResponseMessage response = await client.GetAsync(targetUrl);

            response.EnsureSuccessStatusCode();

            int responseStatusCode = (int)response.StatusCode;

            var result = await response.Content.ReadAsStreamAsync();

            client.Dispose();

            return result;
        }
    }
}
