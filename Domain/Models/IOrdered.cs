using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    /// <summary>
    /// IOrdered is required to order fields in paged collection
    /// </summary>
    public interface IOrdered
    {
        string Name { get; set; }
    }
}
