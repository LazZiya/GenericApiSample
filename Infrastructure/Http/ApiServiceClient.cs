using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using System.Collections;
using System.IO;
using System.Linq;

namespace Infrastructure.Http
{
    public class ApiServiceClient : IApiServiceClient
    {
        private readonly HttpClient client;
        private string requestUri { get; set; }

        /// <summary>
        /// creates http client to make api calls
        /// </summary>
        /// <param name="targetController">api controller name e.g. players</param>
        public ApiServiceClient()
        {
            //if (string.IsNullOrEmpty(targetController))
            //    throw new ArgumentNullException(nameof(targetController));

            client = new HttpClient();

            //requestUri = $"api/{targetController}/";
        }

        public void ConfigureOptions(Action<ApiServiceClientOptions> options)
        {
            var ops = new ApiServiceClientOptions();
            options.Invoke(ops);

            client.BaseAddress = ops.BaseAddress ?? new Uri("https://localhost:44376/api/");
            requestUri = ops.TargetController;
        }

        public async Task<bool> AddAsync<T>(T value)
        {
            var task = await client.PostAsJsonAsync(requestUri, value);

            // ReadAsAync requires: Microsoft.AspNet.WebApi.Client nuget package
            return task.IsSuccessStatusCode
                ? task.Content.ReadAsAsync<bool>().Result
                : throw new HttpRequestException(task.ReasonPhrase); ;
        }

        public async Task<bool> DeleteAsync<TKey>(TKey id)
        {
            var task = await client.DeleteAsync($"{requestUri}/{id}");

            return task.IsSuccessStatusCode
                ? task.Content.ReadAsAsync<bool>().Result
                : throw new HttpRequestException(task.ReasonPhrase);
        }

        public async Task<T> GetAsync<T, TKey>(TKey id)
        {
            var task = await client.GetAsync($"{requestUri}/{id}");

            return task.IsSuccessStatusCode
                ? task.Content.ReadAsAsync<T>().Result
                : throw new HttpRequestException(task.ReasonPhrase);
        }

        public async Task<(IEnumerable<T>, int)> GetListAsync<T>(int pageNo, int pageSize)
        {
            var task = await client.GetAsync($"{requestUri}/list/{pageNo}-{pageSize}");

            return task.IsSuccessStatusCode
                ? task.Content.ReadAsAsync<(IEnumerable<T>, int)>().Result
                : throw new HttpRequestException(task.ReasonPhrase);
        }

        public async Task<bool> UpdateAsync<T, TKey>(TKey id, T value)
        {
            var task = await client.PutAsJsonAsync<T>($"{requestUri}/{id}", value);

            return task.IsSuccessStatusCode
                ? task.Content.ReadAsAsync<bool>().Result
                : throw new HttpRequestException(task.ReasonPhrase);
        }
    }
}
