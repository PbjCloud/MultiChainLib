using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiChainLib
{
    public class AssetResponse
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("genesistxid")]
        public string GenesisTxId { get; set; }

        [JsonProperty("assetref")]
        public string AssetRef { get; set; }

        [JsonProperty("multiple")]
        public int Multiple { get; set; }

        [JsonProperty("units")]
        public decimal Units { get; set; }

        [JsonProperty("details")]
        public object Details { get; set; }

        [JsonProperty("issueqty")]
        public decimal IssueQty { get; set; }

        [JsonProperty("issueraw")]
        public long IssueRaw { get; set; }
    }
}
