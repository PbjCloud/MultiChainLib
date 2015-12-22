using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiChainLib
{
    public class VerboseTransactionResponse
    {
        [JsonProperty("hex")]
        public string Hex { get; set; }

        [JsonProperty("txid")]
        public string TxId { get; set; }

        [JsonProperty("version")]
        public int Version { get; set; }

        [JsonProperty("locktime")]
        public string LockTime { get; set; }

        [JsonProperty("vin")]
        public List<TransactionVin> Vin { get; set; }

        [JsonProperty("vout")]
        public List<TransactionVout> Vout { get; set; }

        [JsonProperty("data")]
        public List<string> Data { get; set; }

        [JsonIgnore]
        public byte[] DataAsBytes
        {
            get
            {
                throw new NotImplementedException("This operation has not been implemented.");
            }
        }

        public VerboseTransactionResponse()
        {
            this.Vin = new List<TransactionVin>();
            this.Vout = new List<TransactionVout>();
            this.Data = new List<string>();
        }

        public byte[] GetDataAsBytes(int index)
        {
            return MultiChainClient.ParseHexString(this.Data[index]);
        }
    }
}
