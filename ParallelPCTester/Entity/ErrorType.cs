using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelPCTester.Entity;
internal enum ErrorType 
{
    [Description("清除flag失败-未知flag")]
    FlagFail_Unknown = -8,

    [Description("清除flag失败-仍为故障")]
    FlagFail_StillFault = -7,

    [Description("清除flag失败-仍为完成")]
    FlagFail_StillDone = -6,

    [Description("清除flag失败-电机正忙")]
    FlagFail_Busy = -5,

    [Description("清除flag失败-通信故障")]
    FlagFail_Communication = -4,

    [Description("任务没有开始")]
    MissionNotStarted = -3,

    [Description("Modbus通信错误")]
    ModbusError = -2,

    [Description("等待执行超时")]
    WaitActionTimeout = -1,

    [Description("无错误")]
    NoError = 0,

    [Description("执行器故障")]
    ActuatorFault = 1,

    [Description("内部执行超时")]
    InternalTimeout = 2,

    [Description("计数错误")]
    CounterError = 3,

    [Description("对准修正错误")]
    AimError = 4,

    [Description("回零失败")]
    BackZeroFail = 5,

    [Description("不支持的命令")]
    UnsupportedCommand = 6,

    [Description("急停触发")]
    EmergencyStop = 0x10,

    [Description("限位触发")]
    LimitTriggered = 0x20,

    [Description("未知错误")]
    UnknownError = 0xF0,

    [Description("临时错误")]
    TemporaryError = 0xFF
}
