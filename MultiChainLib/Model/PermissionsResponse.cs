using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiChainLib
{
    public class PermissionsResponse
    {
        [JsonProperty("connect")]
        public bool Connect { get; set; }

        [JsonProperty("send")]
        public bool Send { get; set; }

        [JsonProperty("receive")]
        public bool Receive { get; set; }

        [JsonProperty("issue")]
        public bool Issue { get; set; }

        [JsonProperty("mine")]
        public bool Mine { get; set; }

        [JsonProperty("admin")]
        public bool Admin { get; set; }

        [JsonProperty("startblock")]
        public long StartBlock { get; set; }

        [JsonProperty("endblock")]
        public long EndBlock { get; set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }
    }
}
