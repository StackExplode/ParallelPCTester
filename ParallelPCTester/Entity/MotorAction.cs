using ParallelPCTester.BLL;
using ParallelPCTester.Driver;
using ParallelPCTester.Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ParallelPCTester.Entity;
internal class MotorAction : IAction
{
    ModbusParser modbus;
    protected MotorActionParam action;
    Stopwatch actiontimer = new Stopwatch();

    public byte Address => action.Motor.ToByte();
    public MotorAddress Motor => action.Motor;
    private ushort[] actparamter => action.Parameter;
    public string Description => action.FriendlyName;

    protected int sendspan = 100;
    int maxtime;

    public MotorAction(MotorActionParam action)
    {
        this.action = action;
    }

    public async Task<ErrorType> ClearFlags(bool clearerror = false,bool requery = false)
    {
        const int maxretry = 5;
        int retry = 0;
        int rt = 0;
        while (retry++ < maxretry)
        {
            await Task.Delay(sendspan);
            await modbus.WriteRegs4x(Address, 501, [0xA5, 0xA5]);
            if(clearerror)
            {
                await Task.Delay(sendspan);
                await modbus.WriteRegs4x(Address, 506, [0xA5, 0xA5]);
            }
            if (!requery)
                return ErrorType.NoError;
            await Task.Delay(sendspan);
            var response = await modbus.QueryReg4x(Address, 411, 1);
            if (!response.Valid || !response.NoError)
                rt = -4;
            else if ((response.Data[1] & 0x03) == 1)
                rt = -5;
            else if ((response.Data[1] & 0x03) == 2)
                rt = -6;
            else if ((response.Data[1] & 0x03) == 3)
                rt = -7;
            else if ((response.Data[1] & 0x03) != 0)
                rt = -8;
            else
            {
                rt = 0;
                break;
            }
            Logger.AppendLine($"清flag失败，已尝试{retry}/{maxretry}次！", "gray");
        }

        return (ErrorType)rt;
    }

    public async Task<ErrorType> SetActionParameter()
    {
        await Task.Delay(sendspan);
        var response = await modbus.WriteRegs4x(Address, 400, actparamter.ToByteArray());
        if (!response.Valid || !response.NoError)
            return ErrorType.ModbusError;
        return ErrorType.NoError;
    }

    public async Task<ErrorType> StartAction()
    {
        qfailcount = 0;
        await Task.Delay(sendspan);
        var response = await modbus.WriteRegs4x(Address, 500, [0xA5, 0xA5]);
        if (!response.Valid || !response.NoError)
            return ErrorType.ModbusError;
        return ErrorType.NoError;
    }

    private int qfailcount = 0;
    public async Task<(ErrorType Error, bool Finished)> PoolOnce()
    {
        await Task.Delay(sendspan);
        var response = await modbus.QueryReg4x(Address, 411, 2);
        if (!response.Valid || !response.NoError)
        {
            if (qfailcount++ > 5)
                return (ErrorType.ModbusError, true);
            else
                return (ErrorType.NoError, false);
        }
        qfailcount = 0;
        var data = response.Data;
        var state = data[1] & 0x03;
        switch (state)
        {
            case 0: return (ErrorType.MissionNotStarted, true);
            case 1: break;
            case 2:
                actiontimer.Stop();
                return (ErrorType.NoError, true);
            case 3: return ((ErrorType)(data[3] + data[4] * 0xFF), true);
        }
        if(actiontimer.ElapsedMilliseconds > maxtime)
        {
            return (ErrorType.WaitActionTimeout, false);
        }
        return (ErrorType.NoError, false);
    }

    public async Task<ErrorType> ExcuteStep(int maxtime = 30000, int queryspan = 100)
    {
        ErrorType rt = ErrorType.NoError;
        rt = await ClearFlags();
        if (rt != ErrorType.NoError)
            return rt;
        rt = await SetActionParameter();
        if (rt != ErrorType.NoError)
            return rt;
        rt = await StartAction();
        if (rt != ErrorType.NoError)
            return rt;
        Stopwatch sw = new Stopwatch();
        sw.Start();
        while (sw.ElapsedMilliseconds < maxtime)
        {
            await Task.Delay(queryspan);
            var (err, finished) = await PoolOnce();
            if (err != ErrorType.NoError)
                return err;
            if (finished)
                return ErrorType.NoError;
        }
        return rt;
    }

    public async Task<ErrorType> PreExecute()
    {
        Logger.AppendLine($"开始为{Description}任务清flag/下发参数...", "deepskyblue");
        ErrorType rt = ErrorType.NoError;
        actiontimer.Restart();
        rt = await ClearFlags();
        if (rt != ErrorType.NoError)
            return rt;
        rt = await SetActionParameter();
        if (rt != ErrorType.NoError)
            return rt;
        rt = await StartAction();
        if (rt != ErrorType.NoError)
            return rt;
        Logger.AppendLine($"任务{Description}正式开始执行...", "deepskyblue");
        
        return rt;
    }

    public void SetEnvironment(ActionEnvironment env)
    {
        modbus = new ModbusParser(env.Port);
        sendspan = env.SendSpan;
        maxtime = env.MaxActionTime;
    }

    public virtual async Task<ErrorType> PostExecute() 
    { 
        Logger.PrintRunTime(actiontimer.ElapsedMilliseconds);
        return ErrorType.NoError; 
    }
}


