using ParallelPCTester.Entity;
using ParallelPCTester.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelPCTester.BLL;
internal static class MotorParamBuilder
{
    public static MotorActionParam BackZero(MotorAddress motor)
    {
        return new MotorActionParam($"{motor.GetDescription()}回零",
            motor,
            [0x02, 0x00]);
    }

    public static MotorActionParam ClawAction(MotorAddress motor, ClawActionType act)
    {
        return new MotorActionParam($"{motor.GetDescription()}{act.GetDescription()}",
            motor,
            [0x07, act.ToUShort()]);
    }

    public static MotorActionParam MoveTo(MotorAddress motor, ushort pos, bool iscard, bool istop = false)
    {
        string cardstr = iscard ? "卡格口" : "证格口";
        if (istop)
            cardstr = "顶部卡格口";
        return new MotorActionParam($"{motor.GetDescription()}到{pos}号位置({cardstr})",
            motor,
            [0x03, pos, 0, 0, (ushort)(iscard ? (istop ? 21 : 1) : 0)]);
    }

    public static MotorActionParam MoveToSpec<T>(MotorAddress motor, T spec) where T : Enum
    {
        return new MotorActionParam($"{motor.GetDescription()}到{spec.GetDescription()}",
            motor,
            [0x03, spec.ToUShort(), 0, 0, 0]);
    }

    public static MotorActionParam Roll(MotorAddress motor, RollType roll)
    {
        ushort rollnum = roll.ToUShort();
        return new MotorActionParam($"{motor.GetDescription()}{roll.GetDescription()}",
            motor,
            [0x06, (ushort)(rollnum & 0x0F), (ushort)(rollnum >> 4)]);
    }

    public static MotorActionParam LiftFastMove(ushort pos, bool iscard, bool istop)
    {
        MotorAddress motor = MotorAddress.Lift;
        if (istop)
            pos--;
        string cardstr = iscard ? "卡格口" : "证格口";
        return new MotorActionParam($"{motor.GetDescription()}快速运动到{pos}号位置而不对准({cardstr})",
            motor,
            [0x03, pos, 0, 0, (ushort)(iscard ? 31 : 30)]);
    }

    public static MotorActionParam LiftAim(ushort pos, bool iscard, bool istop)
    {
        MotorAddress motor = MotorAddress.Lift;
        string cardstr = iscard ? "卡格口" : "证格口";
        return new MotorActionParam($"{motor.GetDescription()}单独对准({cardstr})",
            motor,
            [0x03, pos, 0, 0, (ushort)(iscard ? (istop ? 45 : 41) : 40)]);
    }

    public static MotorActionParam LiftSink(bool iscard)
    {
        MotorAddress motor = MotorAddress.Lift;
        string cardstr = iscard ? "卡格口" : "证格口";
        return new MotorActionParam($"{motor.GetDescription()}无条件下沉({cardstr})",
            motor,
            [0x03, 0, 0, 0, (ushort)(iscard ? 11 : 10)]);
    }

}

internal class MotorActionParam
{
    public MotorAddress Motor { get; set; }
    public string FriendlyName { get; set; }
    public ushort[] Parameter { get; set; }

    public MotorActionParam(string friendlyName, MotorAddress motor, ushort[] parameter)
    {
        FriendlyName = friendlyName;
        Parameter = parameter;
        Motor = motor;
    }
}