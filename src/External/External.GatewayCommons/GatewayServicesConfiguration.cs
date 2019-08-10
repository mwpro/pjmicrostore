using System.Collections.Generic;

namespace External.GatewayCommons
{
    public class GatewayServicesConfiguration
    {
        public Dictionary<string, GatewayServiceConfiguration> GatewayServices { get; set; }

        public class GatewayServiceConfiguration
        {
            public string DownstreamHost { get; set; }
            public int DownstreamPort { get; set; }
        }
    }
}