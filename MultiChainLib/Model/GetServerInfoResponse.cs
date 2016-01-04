using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiChainLib
{
    public class GetServerInfoResponse
    {
        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("availableMethods")]
        public List<string> AvailableMethods { get; set; }

        public GetServerInfoResponse()
        {
            this.AvailableMethods = new List<string>();
        }
    }
}
