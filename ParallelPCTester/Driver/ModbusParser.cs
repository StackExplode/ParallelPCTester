using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelPCTester.Driver
{
    internal class ModbusParser
    {

        public event DataTransFinishedDlg OnDataSent;
        public event DataTransFinishedDlg OnDataReceived;

        private JSerialPort port;
        private ushort lastQueryReg;
        public ushort LastQueryReg => lastQueryReg;

        public bool IgnoreCRC { get; set; } = false;

        public ModbusParser(JSerialPort port)
        {
            this.port = port;
        }

        public bool IsPortOpen => port.IsOpen;

        public static byte[] CalculateCRC16(byte[] data)
        {
            //Calculate CRC16   
            ushort crc = 0xFFFF;
            for (int i = 0; i < data.Length - 2; i++)
            {
                crc ^= data[i];
                for (int j = 0; j < 8; j++)
                {
                    if ((crc & 0x0001) != 0)
                    {
                        crc >>= 1;
                        crc ^= 0xA001;
                    }
                    else
                    {
                        crc >>= 1;
                    }
                }
            }
            return new byte[] { (byte)(crc >> 8), (byte)(crc & 0xFF) };
        }

        public static bool ValidateData(byte addr, byte funcode, byte[] data, bool ignorecrc = false)
        {
            if(data is null || data.Length < 5)
                return false;
            if (addr != data[0] || (data[1] & 0x7F) != funcode )
            {
                return false;
            }

            byte[] crc = CalculateCRC16(data);
            if (!ignorecrc && (crc[0] != data[^2] || crc[1] != data[^1]))
            {
                return false;
            }
            return true;
        }

        public async Task SendData(byte addr,byte funcode, byte[] data)
        {
            byte[] buffer = [addr, funcode , ..data  , 0, 0];
            byte[] crc = CalculateCRC16(buffer);
            buffer[^2] = crc[0];
            buffer[^1] = crc[1];
            await port.SendDataAsync(buffer);
            OnDataSent?.Invoke(buffer,true, false);
        }

        public byte[] DrySend(byte addr,byte funcode, byte[] data)
        {
            byte[] buffer = [addr, funcode, .. data, 0, 0];
            byte[] crc = CalculateCRC16(buffer);
            buffer[^2] = crc[0];
            buffer[^1] = crc[1];
            return buffer;
        }

        public async Task<ModbusData> QueryReg4x(byte addr, ushort reg, ushort count)
        {
            byte[] data = new byte[] { (byte)(reg >> 8), (byte)(reg & 0xFF), (byte)(count >> 8), (byte)(count & 0xFF) };
            lastQueryReg = reg;
            port.DiscardAllBuffer();
            await SendData(addr, 0x03, data);
            byte[] rec = await port.ReadDataAsync();
            bool valid = ValidateData(addr, 0x03, rec, this.IgnoreCRC);
            bool noerror = valid && ((rec[1] & 0x80) == 0);
            OnDataReceived?.Invoke(rec, valid, !noerror);
            return new ModbusData
            {
                Addr = rec?[0] ?? 0,
                Funcode = rec?[1] ?? 0,
                Data = rec?[2..^2],
                Valid = valid,
                NoError = noerror,
                Raw = rec
            };
        }

        public byte[] QueryReg4xDryrun(byte addr, ushort reg, ushort count)
        {
            byte[] data = [(byte)(reg >> 8), (byte)(reg & 0xFF), (byte)(count >> 8), (byte)(count & 0xFF)];
            return DrySend(addr,0x03,data);
        }

        public async Task<ModbusData> WriteRegs4x(byte addr, ushort reg, byte[] data)
        {
            ushort count = (ushort)(data.Length / 2);
            byte[] buffer = [(byte)(reg >> 8), (byte)(reg & 0xFF), (byte)(count >> 8), (byte)(count & 0xFF), (byte)(data.Length), ..data ];
            lastQueryReg = reg;
            port.DiscardAllBuffer();
            await SendData(addr, 0x10, buffer);
            byte[] rec = await port.ReadDataAsync();
            bool valid = ValidateData(addr, 0x10, rec, this.IgnoreCRC);
            bool noerror = valid && ((rec[1] & 0x80) == 0);
            OnDataReceived?.Invoke(rec, valid, !noerror);
            return new ModbusData
            {
                Addr = rec?[0] ?? 0,
                Funcode = rec?[1] ?? 0,
                Data = rec?[2..^2],
                Valid = valid,
                NoError = noerror,
                Raw = rec
            };
        }

        public byte[] WriteRegs4xDryrun(byte addr, ushort reg, byte[] data)
        {
            ushort count = (ushort)(data.Length / 2);
            byte[] buffer = [(byte)(reg >> 8), (byte)(reg & 0xFF), (byte)(count >> 8), (byte)(count & 0xFF), (byte)(data.Length), .. data];
            return DrySend(addr,0x10,buffer);
        }

        public static string ByteArrayToString(byte[] data)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in data)
            {
                sb.Append(b.ToString("X2"));
                sb.Append(" ");
            }
            return sb.ToString();
        }
    }

    internal struct ModbusData
    {
        public byte Addr;
        public byte Funcode;
        public byte[] Data;
        public bool Valid;
        public bool NoError;
        public byte[] Raw;
        public bool IsTimeout => Data is null;
    }
}
