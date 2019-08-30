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
        private HttpClient client;
        private string requestUri;

        /// <summary>
        /// creates http client to make api calls
        /// </summary>
        /// <param name="targetController">api controller name e.g. players</param>
        public ApiServiceClient(string targetController)
        {
            if (string.IsNullOrEmpty(targetController))
                throw new ArgumentNullException(nameof(targetController));

            client = new HttpClient()
            {
                BaseAddress = new Uri("https://localhost:44376/")
            };

            requestUri = $"api/{targetController}/";
        }

        public async Task<bool> AddAsync<T>(T value)
        {
            var content = new StringContent(JsonConvert.SerializeObject(value));
            var task = await client.PostAsync(requestUri, content);

            // ReadAsAync requires: Microsoft.AspNet.WebApi.Client nuget package
            return task.IsSuccessStatusCode
                ? task.Content.ReadAsAsync<bool>().Result 
                : false;
        }

        public async Task<bool> DeleteAsync<TKey>(TKey id)
        {
            var task = await client.DeleteAsync($"{requestUri}/{id}");

            return task.IsSuccessStatusCode
                ? task.Content.ReadAsAsync<bool>().Result
                : false;
        }

        public async Task<T> GetAsync<T, TKey>(TKey id)
        {
            var task = await client.GetAsync($"{requestUri}/{id}");

            return task.IsSuccessStatusCode
                ? task.Content.ReadAsAsync<T>().Result
                : throw new HttpRequestException("object not found!");
        }

        public (Task<IEnumerable<T>>, Task<double>) GetListAsync<T>(int pageNo, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync<T, TKey>(TKey id, T value)
        {
            throw new NotImplementedException();
        }
    }
}
