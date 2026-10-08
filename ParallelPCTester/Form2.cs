using ParallelPCTester.BLL;
using ParallelPCTester.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ParallelPCTester.Entity;
using ParallelPCTester.Helper;
using System.Diagnostics;

namespace ParallelPCTester;
public partial class Form2 : Form
{
    JSerialPort port;
    ActionEnvironment env;
    public Form2(ActionEnvironment env)
    {
        InitializeComponent();
        this.env = env;
        this.port = env.Port;
    }

    bool claw_pp = true;
    private async void button1_Click(object sender, EventArgs e)
    {
        claw_pp = true;
        int pos = 0;
        while (claw_pp)
        {
            await Task.Delay(20);
            var para = MotorParamBuilder.MoveToSpec(MotorAddress.ScrewLead, (ScrewLeadPosType)pos);
            MotorAction m = new MotorAction(para);
            m.SetEnvironment(env);
            var rt = await m.ExcuteStep();
            if (rt != ErrorType.NoError)
                throw new Exception(Name + "执行失败:" + rt.GetDescription());
            pos = (++pos) % 2;
        }
    }

    private void button2_Click(object sender, EventArgs e)
    {
        claw_pp = false;
    }

    private async void button3_Click(object sender, EventArgs e)
    {
        ModbusParser modbus = new ModbusParser(port);
        int n = 10;
        Logger.Clear();
        Program.gtimer.Restart();
        while (n-- > 0)
        {
            Logger.AppendLine($"第{10 - n}次测试开始！", "Orange");
            var rt = await modbus.QueryReg4x(0x01, 1, 4);
            Logger.AppendLine($"接收数据：{rt.Data.ToHexString()}");
            Logger.AppendLine($"第{10 - n}次测试结束！", "Green");
            Logger.AppendLine();
        }
        Program.gtimer.Stop();
    }

    private async void button4_Click(object sender, EventArgs e)
    {
        button4.Enabled = false;
        ModbusParser modbus = new ModbusParser(port);
        await modbus.WriteRegs4x(8, 501, [0xA5, 0xA5]);
        await modbus.WriteRegs4x(8, 506, [0xA5, 0xA5]);
        await modbus.WriteRegs4x(8, 400, [0x02, 0x00]);
        await modbus.WriteRegs4x(8, 500, [0xA5, 0xA5]);
        button4.Enabled = true;
    }

    private void button5_Click(object sender, EventArgs e)
    {
        Config cfg = JSONParserHelper.ParseFile<Config>("config.json");
        MessageBox.Show($"Name={cfg.Name},Value={cfg.Value}");
    }
}
