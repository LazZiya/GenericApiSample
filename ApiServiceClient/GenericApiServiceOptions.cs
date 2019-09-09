using System;

namespace ApiServiceClient
{
    public class GenericApiServiceOptions
    {
        public string TargetController { get; set; }
        public Uri BaseAddress { get; set; }
    }
}
