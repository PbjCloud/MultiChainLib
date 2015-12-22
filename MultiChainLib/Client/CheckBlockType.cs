using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiChainLib
{
    public enum CheckBlockType
    {
        ReadFromDisk = 0,
        EnsureEachBlockIsValid = 1,
        CheckCanReadUndoFiles = 2,
        TestEachBlockUndo = 3,
        ReconnectUndoneBlocks = 4
    }
}
