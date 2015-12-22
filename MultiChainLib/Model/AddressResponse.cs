using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiChainLib
{
    public class AddressResponse
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("ismine")]
        public bool IsMine { get; set; }

        [JsonProperty("iswatchonly")]
        public bool IsWatchOnly { get; set; }

        [JsonProperty("isscript")]
        public bool IsScript { get; set; }

        [JsonProperty("pubkey")]
        public string PubKey { get; set; }

        [JsonProperty("iscompressed")]
        public bool IsCompressed { get; set; }

        [JsonProperty("account")]
        public string Account { get; set; }
    }
}
