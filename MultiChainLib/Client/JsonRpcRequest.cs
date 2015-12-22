using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiChainLib
{
    public class JsonRpcRequest
    {
        public Dictionary<string, object> Values { get; private set; }

        public JsonRpcRequest()
        {
            this.Values = new Dictionary<string, object>();
        }

        internal string Method
        {
            get
            {
                return this.GetValue<string>("method");
            }
            set
            {
                this.SetValue("method", value);
            }
        }

        internal object[] Params
        {
            get
            {
                return this.GetValue<object[]>("params");
            }
            set
            {
                this.SetValue("params", value);
            }
        }


        internal int Id
        {
            get
            {
                return this.GetValue<int>("id");
            }
            set
            {
                this.SetValue("int", value);
            }
        }

        internal string ChainName
        {
            get
            {
                return this.GetValue<string>("chain_name");
            }
            set
            {
                this.SetValue("chain_name", value);
            }
        }

        internal string ChainKey
        {
            get
            {
                return this.GetValue<string>("chain_key");
            }
            set
            {
                this.SetValue("chain_key", value);
            }
        }

        private void SetValue(string name, object value)
        {
            this.Values[name] = value;
        }

        public T GetValue<T>(string name)
        {
            if (this.Values.ContainsKey(name))
                return (T)this.Values[name];
            else
                return default(T);
       }

    }
}
