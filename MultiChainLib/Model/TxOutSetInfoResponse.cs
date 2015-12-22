using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiChainLib
{
    public class TxOutSetInfoResponse
    {
        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("bestblock")]
        public string BestBlock { get; set; }

        [JsonProperty("transactions")]
        public int Transactions { get; set; }

        [JsonProperty("txouts")]
        public int TxOuts { get; set; }

        [JsonProperty("bytes_serialized")]
        public long BytesSerialized { get; set; }

        [JsonProperty("hash_serialized")]
        public string HashSerialized { get; set; }

        [JsonProperty("total_amount")]
        public decimal TotalAmount { get; set; }
    }
}
