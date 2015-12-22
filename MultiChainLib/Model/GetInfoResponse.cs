using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiChainLib
{
    public class GetInfoResponse
    {
        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("protocolversion")]
        public int ProtocolVersion { get; set; }

        [JsonProperty("chainname")]
        public string ChainName { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("protocol")]
        public string protocol { get; set; }

        [JsonProperty("port")]
        public int Port { get; set; }

        [JsonProperty("setupblocks")]
        public int SetupBlocks { get; set; }

        [JsonProperty("nodeaddress")]
        public string NodeAddress { get; set; }

        [JsonProperty("walletversion")]
        public int WalletVersion { get; set; }

        [JsonProperty("balance")]
        public decimal Balance { get; set; }

        [JsonProperty("blocks")]
        public int Blocks { get; set; }

        [JsonProperty("timeoffset")]
        public int TimeOffset { get; set; }

        [JsonProperty("connections")]
        public int Connections { get; set; }

        [JsonProperty("proxy")]
        public string Proxy { get; set; }

        [JsonProperty("difficulty")]
        public decimal Difficulty { get; set; }

        [JsonProperty("testnet")]
        public bool TestNet { get; set; }

        [JsonProperty("keypoololdest")]
        public long KeypoolOldest { get; set; }

        [JsonProperty("keypoolsize")]
        public int KeypoolSize { get; set; }

        [JsonProperty("paytxfee")]
        public decimal PayTxFee { get; set; }

        [JsonProperty("relayfee")]
        public decimal RelayFee { get; set; }

        [JsonProperty("errors")]
        public string Errors { get; set; }
    }
}
