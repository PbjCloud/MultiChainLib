using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiChainLib
{
    public class ChainTipResponse
    {
        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("hash")]
        public string Hash { get; set; }

        [JsonProperty("branchlen")]
        public int BranchLen { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
