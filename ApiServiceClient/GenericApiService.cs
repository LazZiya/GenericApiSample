using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiServiceClient
{
    // Typed client service
    // read more here: 
    // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/http-requests?view=aspnetcore-2.2#typed-clients
    public class GenericApiService
    {
        private readonly HttpClient _client;
        private string requestUri { get; set; }

        /// <summary>
        /// creates http client to make api calls
        /// </summary>
        /// <param name="targetController">api controller name e.g. players</param>
        public GenericApiService(HttpClient client)
        {
            _client = client;
        }

        public void ConfigureOptions(Action<GenericApiServiceOptions> options)
        {
            var ops = new GenericApiServiceOptions();
            options.Invoke(ops);

            if(_client.BaseAddress == null)
                _client.BaseAddress = ops.BaseAddress;

            requestUri = ops.TargetController;
        }

        public async Task<bool> AddAsync<T>(T value)
        {
            var task = await _client.PostAsJsonAsync(requestUri, value);

            // ReadAsAync requires: Microsoft.AspNet.WebApi.Client nuget package
            return task.IsSuccessStatusCode
                ? task.Content.ReadAsAsync<bool>().Result
                : throw new HttpRequestException(task.ReasonPhrase); ;
        }

        public async Task<bool> DeleteAsync<TKey>(TKey id)
        {
            var task = await _client.DeleteAsync($"{requestUri}/{id}");

            return task.IsSuccessStatusCode
                ? task.Content.ReadAsAsync<bool>().Result
                : throw new HttpRequestException(task.ReasonPhrase);
        }

        public async Task<T> GetAsync<T, TKey>(TKey id)
        {
            var task = await _client.GetAsync($"{requestUri}/{id}");

            return task.IsSuccessStatusCode
                ? task.Content.ReadAsAsync<T>().Result
                : throw new HttpRequestException(task.ReasonPhrase);
        }

        public async Task<(IEnumerable<T>, int)> GetListAsync<T>(int pageNo, int pageSize)
        {
            var task = await _client.GetAsync($"{requestUri}/list/{pageNo}-{pageSize}");

            return task.IsSuccessStatusCode
                ? task.Content.ReadAsAsync<(IEnumerable<T>, int)>().Result
                : throw new HttpRequestException(task.ReasonPhrase);
        }

        public async Task<bool> UpdateAsync<T, TKey>(TKey id, T value)
        {
            var task = await _client.PutAsJsonAsync<T>($"{requestUri}/{id}", value);

            return task.IsSuccessStatusCode
                ? task.Content.ReadAsAsync<bool>().Result
                : throw new HttpRequestException(task.ReasonPhrase);
        }
    }
}
