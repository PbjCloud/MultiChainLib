using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiChainLib
{
    public class MempoolInfoResponse
    {
        [JsonProperty("size")]
        public long Size { get; set; }

        [JsonProperty("bytes")]
        public long Bytes { get; set; }
    }
}
