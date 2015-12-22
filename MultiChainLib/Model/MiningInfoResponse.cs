using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiChainLib
{
    public class MiningInfoResponse
    {
        [JsonProperty("blocks")]
        public int Blocks { get; set; }

        [JsonProperty("currentblocksize")]
        public int CurrentBlockSize { get; set; }

        [JsonProperty("difficulty")]
        public decimal Difficulty { get; set; }

        [JsonProperty("errors")]
        public string Errors { get; set; }

        [JsonProperty("genproclimit")]
        public int GenProcLimit { get; set; }

        [JsonProperty("networkhashps")]
        public int NetworkHashPs { get; set; }

        [JsonProperty("pooledtx")]
        public int PooledTx { get; set; }

        [JsonProperty("testnet")]
        public bool TestNet { get; set; }

        [JsonProperty("chain")]
        public string Chain { get; set; }

        [JsonProperty("generate")]
        public bool Generate { get; set; }

        [JsonProperty("hashespersec")]
        public int HashesPerSec { get; set; }
    }
}
