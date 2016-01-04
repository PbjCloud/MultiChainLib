using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiChainLib
{
    public class UnspentAssetResponse
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("assetref")]
        public string AssetRef { get; set; }

        [JsonProperty("qty")]
        public decimal Qty { get; set; }
    }
}
