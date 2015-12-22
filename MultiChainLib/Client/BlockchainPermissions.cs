using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiChainLib
{
    [Flags]
    public enum BlockchainPermissions
    {
        None = 0,
        Connect = 1,
        Send = 2,
        Receive = 4,
        Issue = 8,
        Mine = 16,
        Admin = 32
    }
}
