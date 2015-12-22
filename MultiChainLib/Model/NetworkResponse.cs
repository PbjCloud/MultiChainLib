using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiChainLib
{
    public class NetworkResponse
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("limited")]
        public bool Limited { get; set; }

        [JsonProperty("reachable")]
        public bool Reachable { get; set; }

        [JsonProperty("proxy")]
        public string Proxy { get; set; }
    }
}
