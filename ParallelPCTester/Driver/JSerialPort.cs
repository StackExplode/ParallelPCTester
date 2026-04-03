using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO.Ports;

namespace ParallelPCTester.Driver
{
    public delegate void DataTransFinishedDlg(byte[] data,bool valid,bool error);
    public class JSerialPort
    {
        public static string[] EnumPorts()
        {
            return SerialPort.GetPortNames();
        }

        private SerialPort port;


        public string PortName
        {
            get => port.PortName;
            set => port.PortName = value;
        }
        public int BaudRate
        {
            get => port.BaudRate;
            set => port.BaudRate = value;
        }
        public int Timeout { get; set; } = 50;

        public int LostTimeout { get; set; } = 5000;

        public bool IsOpen => port.IsOpen;

        public JSerialPort()
        {
            port = new SerialPort();
            port.StopBits = StopBits.One;
            port.Parity = Parity.None;
            port.ReadTimeout = 10;
            port.WriteTimeout = 10;
            port.ReadBufferSize = 1024;
            port.WriteBufferSize = 1024;
        }

        public void Open()
        {
            port.Open();
        }

        public void Close()
        {
            port.Close();
        }

        public void DiscardAllBuffer()
        {
            port.DiscardInBuffer();
            port.DiscardOutBuffer();
        }

        public async Task SendDataAsync(byte[] data)
        {
            try
            {
                await port.BaseStream.WriteAsync(data, 0, data.Length);
            }
            catch (OperationCanceledException) { }
            
        }
        public async Task<byte[]> ReadDataAsync_New(int maxLength = 1024)
        {
            byte[] buffer = new byte[maxLength];
            int count = 0;

            var stream = port.BaseStream;

            // 等首字节
            var firstByteTask = stream.ReadAsync(buffer, 0, maxLength);
            if (await Task.WhenAny(firstByteTask, Task.Delay(LostTimeout)) != firstByteTask)
            {
                return null;
            }

            count = firstByteTask.Result;

            var sw = System.Diagnostics.Stopwatch.StartNew();

            while (true)
            {
                if (sw.ElapsedMilliseconds > Timeout)
                    break;

                if (port.BytesToRead > 0)
                {
                    int read = await stream.ReadAsync(buffer, count, maxLength - count);
                    count += read;

                    sw.Restart(); // 收到数据就重置间隔计时
                }
                else
                {
                    await Task.Delay(1); // 小延迟避免空转
                }
            }

            return count == 0 ? null : buffer[..count];
        }


        public async Task<byte[]> ReadDataAsync(int maxlength = 1024)
        {
            byte[] buffer = new byte[maxlength];
            int count = 0;
            var cancel0 = new CancellationTokenSource();
            var task01 = port.BaseStream.ReadAsync(buffer, 0, 1,cancel0.Token);
            var task02 = Task.Delay(LostTimeout);
            var finisher0 = await Task.WhenAny(task01, task02);
            if (finisher0 == task01)
            {
                count += task01.Result;
            }
            else
            {
                cancel0.Cancel();
                return null;
            }

            while (true)
            {
                var cancel = new CancellationTokenSource();
                var task1 = port.BaseStream.ReadAsync(buffer, count, 1, cancel.Token);
                var task2 = Task.Delay(Timeout);
                var finisher = await Task.WhenAny(task1, task2);
                if(finisher == task1)
                {
                    count += task1.Result;
                }
                else
                {
                    cancel.Cancel();
                    break;
                }
            }

            return count == 0 ? null : buffer[0..count];
        }

    }
}
