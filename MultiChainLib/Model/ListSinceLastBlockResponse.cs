using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiChainLib
{
    public class ListSinceLastBlockResponse 
    {
        [JsonProperty("transactions")]
        public List<TransactionResponse> Transactions { get; set; }

        public ListSinceLastBlockResponse()
        {
            this.Transactions = new List<TransactionResponse>();
        }
    }
}
