using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ParallelPCTester.Helper;
internal static class ArrayExtender
{
    public static byte[] ToByteArray(this ushort[] data)
    {
        Span<byte> bytes =  MemoryMarshal.AsBytes(data.AsSpan());
        return bytes.ToArray();
    }

    public static string ToHexString(this byte[] data)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("[");
        foreach (byte b in data)
        {
            sb.Append(b.ToString("X2"));
            sb.Append(",");
        }
        if (data.Length > 0)
            sb.Remove(sb.Length - 1, 1);
        sb.Append("]");
        return sb.ToString();
    }
}
