using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiChainLib
{
    public class ListPermissionsResponse
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("startblock")]
        public long StartBlock { get; set; }

        [JsonProperty("endblock")]
        public long EndBlock { get; set; }
    }
}
