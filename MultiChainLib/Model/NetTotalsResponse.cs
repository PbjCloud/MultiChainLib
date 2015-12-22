using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiChainLib
{
    public class NetTotalsResponse
    {
        [JsonProperty("totalbytesrecv")]
        public long TotalsBytesRecv { get; set; }

        [JsonProperty("totalbytessent")]
        public long TotalsBytesSent { get; set; }

        [JsonProperty("timemillis")]
        public long TimeMillis { get; set; }
    }
}
