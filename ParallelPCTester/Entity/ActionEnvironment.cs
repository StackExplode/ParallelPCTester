using ParallelPCTester.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelPCTester.Entity;
public class ActionEnvironment
{
    public JSerialPort Port { get; set; }
    public int SendSpan { get; set; }
    public int QuerySpan { get; set; }
    public int MaxActionTime { get; set; }
}
