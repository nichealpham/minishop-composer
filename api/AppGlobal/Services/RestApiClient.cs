using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AppGlobal.Services
{
    public abstract class RestApiClient
    {
        private readonly HttpClient client;

        public RestApiClient(HttpClient client)
            => this.client = client;

        public RestApiClient Config(params Action<HttpClient>[] configurators)
        {
            foreach (var configurator in configurators)
                configurator.Invoke(client);
            return this;
        }

        /* Synchronous HTTP request methods */

        public T Request<T>(
            HttpMethod httpMethod, 
            string url, 
            params Action<HttpRequestMessage>[] configurators)
        {
            using var request = new HttpRequestMessage(httpMethod, url);
            foreach (var configurator in configurators)
                configurator.Invoke(request);
            var response = client.SendAsync(request).Result;    // blocks current thread till Result is available
            var json = response.Content.ReadAsStringAsync().Result;
            return json.ToJsonObject<T>();
        }

        public T GetRequest<T>(string url, params Action<HttpRequestMessage>[] configurators)
            => Request<T>(HttpMethod.Get, url, configurators);
        public T PostRequest<T>(string url, params Action<HttpRequestMessage>[] configurators)
            => Request<T>(HttpMethod.Post, url, configurators);

        /* Asynchronous versions */

        public async Task<T> RequestAsync<T>(
            HttpMethod httpMethod,
            string url,
            params Action<HttpRequestMessage>[] configurators)
        {
            using var request = new HttpRequestMessage(httpMethod, url);
            foreach (var configurator in configurators)
                configurator.Invoke(request);
            var response = await client.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();
            return json.ToJsonObject<T>();
        }

        public async Task<T> GetRequestAsync<T>(string url, params Action<HttpRequestMessage>[] configurators)
            => await RequestAsync<T>(HttpMethod.Get, url, configurators);
        public async Task<T> PostRequestAsync<T>(string url, params Action<HttpRequestMessage>[] configurators)
            => await RequestAsync<T>(HttpMethod.Post, url, configurators);
    }

    public static class RestClientExtensions
    {
        /* Serialisation of any object to and from JSON */

        public static string ToJsonString<T>(this T @object)
        {
            var jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;
            jsonSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            return JsonConvert.SerializeObject(@object, jsonSerializerSettings);
        }
        public static T ToJsonObject<T>(this string json)
        {
            var jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;
            return JsonConvert.DeserializeObject<T>(json, jsonSerializerSettings);
        }

        /* HttpRequestMessage convenience extension methods */

        public static HttpRequestMessage AddJsonBody<T>(this HttpRequestMessage request, T @object)
        {
            if (@object != null)
            {
                // Convert object to json and set request content-type accordingly
                var content = new StringContent(@object.ToJsonString());
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                request.Content = content;
            }
            return request;
        }

        public static HttpRequestMessage AddHeaders(this HttpRequestMessage request, Dictionary<string, string> headers)
        {
            if (headers != null && headers.Count != 0)
                foreach (var pair in headers)
                    request.Headers.Add(pair.Key, pair.Value);
            return request;
        }
    }
}
