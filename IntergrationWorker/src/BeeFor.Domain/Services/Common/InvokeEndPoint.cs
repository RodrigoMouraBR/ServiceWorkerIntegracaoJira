using BeeFor.Domain.Entities;
using BeeFor.Domain.Services.Constants;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace BeeFor.Domain.Services.Common
{
    public static class InvokeEndPoint
    {
        public static async Task<(T, List<T>)> Invoke<T>(ConfiguracaoIntegracao configuracaoIntegracao,
                                                        string enPoint, T results, int? parameter, bool isListReturn) where T : class
        {
            try
            {
                var apiBaseUrl = UrlDomain.MontarApiBaseUrl(configuracaoIntegracao.BaseUrlJira);
                string url = string.Empty;

                if (parameter != null)
                {
                    var endPointTratado = URLConstants.metodoInt(enPoint, (int)parameter);
                    url = apiBaseUrl + endPointTratado;
                }
                else
                {
                    url = apiBaseUrl + enPoint;
                }

                var result = await RequestHttp.HTTP_GET(configuracaoIntegracao.Usuario, configuracaoIntegracao.Chave, url);

                if (!isListReturn)
                {
                    var Retconfig1 = await JsonSerializer.DeserializeAsync<T>(result, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    return (Retconfig1, null);
                }
                if (isListReturn)
                {
                    var Retconfig12 = await JsonSerializer.DeserializeAsync<List<T>>(result, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    return (null, Retconfig12);
                }              

                return (null, null);
            }
            catch (Exception e)
            {
                throw new Exception(URLConstants.retornoErro);
            }
        }
    }
}
