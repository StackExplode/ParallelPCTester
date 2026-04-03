using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelPCTester.Entity;

[Flags]
internal enum MissionType
{
    IsCard = 1,
    IsTop = 1<<1,
    IsPick = 1<<2,
    IsBackZero = 1<<3,
    IsCheat = 1 << 4,
}
