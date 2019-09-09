﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Player : IHasId<string>, IOrdered
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
    }
}
