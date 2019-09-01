using System;
using System.Collections.Generic;
using System.Text;

namespace ApiServiceClient
{
    public class HttpServiceClientOptions
    {
        public string TargetController { get; set; }
        public Uri BaseAddress { get; set; }
    }
}
