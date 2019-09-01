using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApiServiceClient
{
    public interface IHttpServiceClient
    {
        Task<T> GetAsync<T, TKey>(TKey id);
        Task<bool> AddAsync<T>(T value);
        Task<bool> UpdateAsync<T, TKey>(TKey id, T value);
        Task<bool> DeleteAsync<TKey>(TKey id);
        Task<(IEnumerable<T>, int)> GetListAsync<T>(int pageNo, int pageSize);
        void ConfigureOptions(Action<HttpServiceClientOptions> p);
    }
}
