using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace WpEmpresas.Services.Helpers
{
    public class ServiceHelper
    {
        public ServiceHelper()
        { }

        public async Task<T> GetAsync<T>(string serverUrl, string resource)
        {
            var result = string.Empty;
            var request = (HttpWebRequest)WebRequest.Create($"{ serverUrl }{ resource }");
            using (var httpResponse = (HttpWebResponse)await request.GetResponseAsync())
            {
                using (var stream = httpResponse.GetResponseStream())
                {
                    using (var reader = new StreamReader(stream))
                    {
                        result = await reader.ReadToEndAsync();
                    }
                }
            }

            var response = JsonConvert.DeserializeObject<T>(result);

            return response;
        }

        public async Task<T> PostAsync<T>(string serverUrl, string resource, object envio)
        {
            var data = JsonConvert.SerializeObject(envio);

            var httpWebRequest = (HttpWebRequest)WebRequest.Create($"{ serverUrl }{ resource }");
            httpWebRequest.ReadWriteTimeout = Timeout.Infinite;
            httpWebRequest.Timeout = Timeout.Infinite;
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(await httpWebRequest.GetRequestStreamAsync()))
            {
                await streamWriter.WriteAsync(data);
                await streamWriter.FlushAsync();
                streamWriter.Close();
            }

            var result = string.Empty;
            var httpResponse = (HttpWebResponse)await httpWebRequest.GetResponseAsync();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = await streamReader.ReadToEndAsync();
            }

            var response = JsonConvert.DeserializeObject<T>(result);

            return response;
        }
    }
}