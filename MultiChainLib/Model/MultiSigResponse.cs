using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiChainLib
{
    public class MultiSigResponse
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("redeemScript")]
        public string RedeemScript { get; set; }
    }
}
