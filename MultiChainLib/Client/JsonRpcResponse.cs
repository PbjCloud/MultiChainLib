using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiChainLib
{
    public class JsonRpcResponse<T>
    {
        [JsonProperty("result")]
        public T Result { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("id")]
        public int? Id { get; set; }

        [JsonIgnore]
        public string RawJson { get; internal set; }

        public void AssertOk()
        {
            if (!(string.IsNullOrEmpty(this.Error)))
                throw new InvalidOperationException("Error(s) occurred: " + this.Error);
        }
    }
}
