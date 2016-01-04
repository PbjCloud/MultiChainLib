using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiChainLib
{
    public class UnspentResponse
    {
        [JsonProperty("txid")]
        public string TxId { get; set; }

        [JsonProperty("vout")]
        public int Vout { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("account")]
        public string Account { get; set; }

        [JsonProperty("scriptPubKey")]
        public string ScriptPubKey { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("Confirmations")]
        public int Confirmations { get; set; }

        [JsonProperty("cansend")]
        public bool CanSend { get; set; }

        [JsonProperty("spendable")]
        public bool Spendable { get; set; }

        [JsonProperty("assets")]
        public List<UnspentAssetResponse> Assets { get; set; }

        [JsonProperty("permissions")]
        public List<PermissionsResponse> Permissions { get; set; }

        public UnspentResponse()
        {
            this.Assets = new List<UnspentAssetResponse>();
            this.Permissions = new List<PermissionsResponse>();
        }
    }
}
