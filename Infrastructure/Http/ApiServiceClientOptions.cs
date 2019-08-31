using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Http
{
    public class ApiServiceClientOptions
    {
        public string TargetController { get; set; }
        public Uri BaseAddress { get; set; }
    }
}
