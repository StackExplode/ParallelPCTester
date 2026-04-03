using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ParallelPCTester.Helper;
internal static class EnumExtender
{
    public static byte ToByte(this Enum value)
    {
        return Convert.ToByte(value);
    }

    public static ushort ToUShort(this Enum value)
    {
        return Convert.ToUInt16(value);
    }

    public static uint ToUInt(this Enum value)
    {
        return Convert.ToUInt32(value);
    }

    public static int ToInt(this Enum value)
    {
        return Convert.ToInt32(value);
    }

    public static string? GetDescription(this Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attr = field.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false).FirstOrDefault() as System.ComponentModel.DescriptionAttribute;
        return attr?.Description;
    }

}
