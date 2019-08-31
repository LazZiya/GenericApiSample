using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class Treasure : IHasId<int>, IOrdered
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Kind { get; set; }
    }
}
