using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelPCTester.Entity;

internal enum MotorAddress : byte
{
    [Description("大旋转")]
    BigRotate = 5,
    [Description("小旋转")]
    SmallRotate = 6,
    [Description("升降")]
    Lift = 7,
    [Description("丝杆/夹爪")]
    ScrewLead = 8,
    [Description("丝杆/夹爪")]
    Claw = 8,
    [Description("卷证")]
    Roll = 11
}
