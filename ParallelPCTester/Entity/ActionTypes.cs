using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelPCTester.Entity;

internal enum ClawActionType : ushort
{
    [Description("闭合")]
    Close = 0x1000,
    [Description("半开")]
    HalfOpen = 0x2000,
    [Description("全开")]
    BigOpen = 0x3000,
    [Description("微开")]
    MicroOpen = 0x4000
}

internal enum LiftSpecPosType : ushort
{
    [Description("出口")]
    Exit = 255,
    [Description("入证口")]
    BookEntrance = 254,
    [Description("入卡口")]
    CardEntrance = 253,
    [Description("批量口")]
    BatchEntrance = 252,
}

internal enum ScrewLeadPosType : ushort
{
    [Description("缩到底")]
    FullBack = 0,
    [Description("入证位")]
    BookEntrance = 1,
    [Description("入卡位")]
    CardEntrance = 2,
    [Description("批量位")]
    BatchEntrance = 3,
    [Description("证格口")]
    BookStore = 4,
    [Description("卡格口")]
    CardStore = 5,
    [Description("出证位")]
    BookExit = 6,
    [Description("出卡位")]
    CardExit = 7,
}

internal enum SmallRoatePosType : ushort
{
    [Description("1号位（正对门）")]
    DoorPos = 1,
    [Description("2号位（激光对准）")]
    LaserPos = 2,
    [Description("3号位（正对格口）")]
    CasePos = 3,
}

internal enum RollType : ushort
{
    [Description("卷入卡")]
    CardIn = 0x11,
    [Description("卷入证")]
    BookIn = 0x22,
    [Description("卷出")]
    Out = 0x13,
    [Description("卷批量口")]
    Batch = 0x24
}

internal enum CaseType : byte
{
    [Description("证格口")]
    BookCase = 0,
    [Description("卡格口")]
    CardCase = 1,
    
}
