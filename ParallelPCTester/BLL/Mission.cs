using ParallelPCTester.Driver;
using ParallelPCTester.Entity;
using ParallelPCTester.Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelPCTester.BLL;

[Obsolete("旧方法，已经淘汰",true)]
internal class Mission
{
    private Queue<MotorActionParam[]> AllActions;
    private JSerialPort port;
    private int sendspan;
    private int queryspan;
    private int actiontimeout;

    public Mission(ActionBuilder builder, JSerialPort port, int sendspan, int queryspan, int actiontimeout)
    {
        //AllActions = builder.Root; //TODO
        this.port = port;
        this.sendspan = sendspan;
        this.queryspan = queryspan;
        this.actiontimeout = actiontimeout;
    }

    private async Task<bool> ClearFlags(LinkedList<MotorAction> motors)
    {
        Logger.AppendLine("清flag阶段...", "deepskyblue");
        foreach (var m in motors)
        {
            Logger.AppendLine($"正在清除{m.Motor.GetDescription()}的flag...");
            var rst = await m.ClearFlags();
            Logger.PrintResult(rst, $"清除{m.Motor.GetDescription()}的flag成功！", "清flag出错！");
            if (rst != ErrorType.NoError)
                return false;
        }
        Logger.AppendLine("全部flag清除成功！", "deepskyblue");
        return true;
    }

    private async Task<bool> SetParameters(LinkedList<MotorAction> motors)
    {
        Logger.AppendLine("参数下发阶段...", "deepskyblue");
        foreach (var m in motors)
        {
            Logger.AppendLine($"正在下发{m.Motor.GetDescription()}的参数...");
            var rst = await m.SetActionParameter();
            Logger.PrintResult(rst, $"下发{m.Motor.GetDescription()}的参数成功！", $"下发{m.Motor.GetDescription()}的参数出错！");
            if (rst != ErrorType.NoError)
                return false;
        }
        Logger.AppendLine($"全部参数下发成功！", "deepskyblue");
        return true;
    }

    private async Task<bool> TriggerExecution(LinkedList<MotorAction> motors)
    {
        foreach (var m in motors)
        {
            var rst = await m.StartAction();
            if (rst != ErrorType.NoError)
            {
                Logger.PrintResult(rst, "", $"触发{m.Motor.GetDescription()}动作启动出错！");
                return false;
            }
        }
        return true;
    }


    public async Task<bool> ExecuteOneStep(MotorActionParam[] acts)
    {
        LinkedList<MotorAction> motors = new LinkedList<MotorAction>();
        foreach (var para in acts)
        {
            //motors.AddLast(new MotorAction(para)); //TODO
        }

        Stopwatch sw = new Stopwatch();
        sw.Start();
        Logger.AppendLine($"开始{(motors.Count > 1 ? "并行" : "")}执行{motors.Count}个操作：", "blue");
        foreach (var m in motors)
        {
            Logger.AppendLine($"\t- {m.Description}");
        }

        var cflag_result = await ClearFlags(motors);    //Step1 清flag
        if (!cflag_result)
            return false;

        var sparam_result = await SetParameters(motors);    //Step2 下发参数
        if (!sparam_result)
            return false;


        Logger.AppendLine("执行阶段...", "deepskyblue");

        var trigger_result = await TriggerExecution(motors);    //Step3 触发执行
        if (!trigger_result)
            return false;

        var current = motors.First!;
        Logger.SpinUpdate(true);
        Stopwatch sw2 = new Stopwatch();
        sw2.Start();
        while (true) //Step4 交替轮询动作执行状态
        {
            var m = current.Value;
            await Task.Delay(queryspan);
            Logger.SpinUpdate();
            var rst = await m.PoolOnce();
            if (rst.Error != ErrorType.NoError)
            {
                Logger.Backspace(1);
                Logger.AppendLine();
                Logger.PrintResult(rst.Error, "", $"执行{m.Description}过程出错！");
                return false;
            }
            var last = current;
            current = current.Next ?? motors.First;
            if (rst.Finished)
            {
                Logger.Backspace(1);
                Logger.AppendLine();
                Logger.Append($"\t- 执行{m.Description}过程完毕！");
                Logger.PrintRunTime(sw.ElapsedMilliseconds);
                motors.Remove(last);
            }
            if (motors.Count == 0)
            {
                break;
            }
            if (sw2.ElapsedMilliseconds > actiontimeout)
            {
                Logger.Backspace(1);
                Logger.AppendLine();
                Logger.AppendLine($"执行超时，仍有{motors.Count}个动作未完成:", "red");
                foreach (var m2 in motors)
                {
                    Logger.AppendLine($"\t- {m2.Description}未完成", "red");
                }
                return false;
            }
        }
        sw.Stop();
        sw2.Stop();
        Logger.AppendLine($"该组操作全部执行完成！", "blue");
        Logger.PrintRunTime(sw.ElapsedMilliseconds,"该组操作共花费：");
        return true;
    }

    public async Task ExecuteAll()
    {
        Thread.CurrentThread.Priority = ThreadPriority.Highest;
        int i = 0;
        foreach (var acts in AllActions)
        {
            Logger.Append($"第{++i}步：");
            var rst = await ExecuteOneStep(acts);
            if (!rst)
            {
                Logger.AppendLine($"执行中断！", "red");
                return;
            }
            Logger.AppendLine();
        }
    }
}
