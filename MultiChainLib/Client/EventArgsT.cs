using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiChainLib
{
    public class EventArgs<T> : EventArgs
    {
        public T Item { get; set; }

        public EventArgs()
        {
        }

        public EventArgs(T item)
        {
            this.Item = item;
        }
    }
}
