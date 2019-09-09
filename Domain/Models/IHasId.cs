using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Models
{
    /// <summary>
    /// This interface allows us to define Id key of any type
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IHasId<TKey> where TKey : IEquatable<TKey>
    {
        TKey Id { get; set; }
    }
}
