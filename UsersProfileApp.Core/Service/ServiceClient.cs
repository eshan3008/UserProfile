using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UsersProfileApp.Core.Service
{
    public class ServiceClient
    {
        public async static Task<T> Request<T>(string requestUrl, HttpMethod httpMethod)
        {
            T result = default(T);
            using (var client = new HttpClient())
            {
                var resp = await client.SendAsync(new HttpRequestMessage(httpMethod, requestUrl));

                if (resp.Content != null)
                {
                    using (var reader = new StreamReader(await resp.Content.ReadAsStreamAsync()))
                    {
                        using (var jsonReader = new JsonTextReader(reader))
                        {
                            var serializer = new JsonSerializer();
                            result = serializer.Deserialize<T>(jsonReader);
                        }
                    }
                }
            }

            return result;
        }
    }
}
