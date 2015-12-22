using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiChainLib
{
    public class BlockchainInfoResponse
    {
        [JsonProperty("chain")]
        public string Chain { get; set; }

        [JsonProperty("chainname")]
        public string ChainName { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("protocol")]
        public string Protocol { get; set; }

        [JsonProperty("setupblocks")]
        public int SetupBlocks { get; set; }

        [JsonProperty("blocks")]
        public int Blocks { get; set; }

        [JsonProperty("headers")]
        public int Headers { get; set; }

        [JsonProperty("bestblockhash")]
        public string BestBlockHash { get; set; }

        [JsonProperty("difficulty")]
        public decimal Difficulty { get; set; }

        [JsonProperty("verificationprogress")]
        public decimal VerificationProgress { get; set; }

        [JsonProperty("chainwork")]
        public string ChainWork { get; set; }
    }
}
